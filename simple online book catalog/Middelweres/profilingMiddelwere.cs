using System.Diagnostics;

namespace simple_online_book_catalog.Middelweres
{
    public class profilingMiddelwere
    {
        private readonly RequestDelegate next;
        private readonly ILogger<profilingMiddelwere> logger;

        public profilingMiddelwere(RequestDelegate next, ILogger<profilingMiddelwere> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context) {

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await next(context);
            stopWatch.Stop();
            logger.LogInformation("Request "+context.Request.Path + " took "+ stopWatch.ElapsedMilliseconds+ "ms");
        }
    }
}
