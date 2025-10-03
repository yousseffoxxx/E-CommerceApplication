using Microsoft.AspNetCore.Http;

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
            // Response Object
            var response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message,                
            };

            // Set status code for Response
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                ValidationException validationException => HandelValidationException(validationException, response),
                _ => StatusCodes.Status500InternalServerError
            };

            response.StatusCode = httpContext.Response.StatusCode;

            // Set Content Type For Response
            //httpContext.Response.ContentType = "application/json";

            // Return object as Json
            await httpContext.Response.WriteAsJsonAsync(response); // automatically changes the content type to "application/json"
        }

        private static int HandelValidationException(ValidationException validationException, ErrorToReturn response)
        {
            response.Errors = validationException.Errors;

            return StatusCodes.Status400BadRequest;
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
