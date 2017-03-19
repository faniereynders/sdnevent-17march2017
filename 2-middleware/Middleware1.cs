using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace middleware
{

    public class Middleware1
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        public Middleware1(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            this.logger = loggerFactory.CreateLogger("Middleware Demo");
        }

        public async Task Invoke(HttpContext context)
        {
            logger.LogInformation("Hello from Middleware 1");
            await next.Invoke(context);
        }
    }
}