using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace middleware
{

    public class Middleware3
    {
        private readonly RequestDelegate next;
        private readonly ILogger<Middleware3> logger;
        public Middleware3(RequestDelegate next, ILogger<Middleware3> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            logger.LogInformation("Hello from Middleware 3");
            await next.Invoke(context);
        }
    }
}