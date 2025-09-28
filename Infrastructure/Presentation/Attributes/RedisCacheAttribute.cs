namespace Presentation.Attributes
{
    internal class RedisCacheAttribute(int _durationInSeconds = 60) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            
            var result = await cacheService.GetCachedValueAsync(cacheKey);

            if(result is not null)
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "Application/Json",
                    StatusCode = (int)HttpStatusCode.OK
                };
                return;
            }

            var resultContext = await next.Invoke();

            if(resultContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.SetCacheValueAsync(cacheKey, okObjectResult, TimeSpan.FromSeconds(_durationInSeconds));
            }              
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path);

            foreach (var item in request.Query.OrderBy(q=>q.Key))
            {
                keyBuilder.Append($"|{item.Key}-{item.Value}");
            }

            return keyBuilder.ToString();
        }
    }
}
