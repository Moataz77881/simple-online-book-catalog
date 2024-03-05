using System.Net;

namespace simple_online_book_catalog.Middelweres
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        // must use InvokeAsync name of method 
        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                //create log exception
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                //return a custom error response

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "something went wrong! we are looking into resolving it"
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
