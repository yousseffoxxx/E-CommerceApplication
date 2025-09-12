namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare(RequestDelegate _next,
        ILogger<CustomExceptionHandlerMiddleWare> _logger)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            // Set status code for Response
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            // Set Content Type For Response
            //httpContext.Response.ContentType = "application/json";

            // Response Object
            var response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            // Return object as Json
            await httpContext.Response.WriteAsJsonAsync(response); // automatically changes the content type to "application/json"
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} Is Not Found"
                };

                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
