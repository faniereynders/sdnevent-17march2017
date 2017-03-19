using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace middleware
{

    public class Middleware2
    {
        private readonly RequestDelegate next;
        private readonly ILogger<Middleware2> logger;
        public Middleware2(RequestDelegate next, ILogger<Middleware2> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            logger.LogInformation("Hello from Middleware 2");
            await next.Invoke(context);
        }
    }
}