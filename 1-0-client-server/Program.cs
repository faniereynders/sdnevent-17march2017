using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace client_server
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole();

            var logger = loggerFactory.CreateLogger<Program>();
            
            var host = new WebHostBuilder()
                .UseKestrel()
                .Configure(app =>
                { 
                    app.Run(async context =>
                    {
                        logger.LogInformation($"Request to {context.Request.Path}");
                        await context.Response.WriteAsync(
                            $"Hello from {context.Request.Path}!\n\n");
                    });
                })
                .Build();

            host.Run();

        }
    }
}
