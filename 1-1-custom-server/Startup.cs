using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApplication8
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            var logger = loggerFactory.CreateLogger<Program>();

            app.MapWhen(c=>c.Request.Path == "/test1", config =>
             {
                 config.Run(async (context) =>
                 {
                     logger.LogInformation("Invoking Test1 endpoint");
                     await context.Response.WriteAsync("Hello from Test1!");
                 });
             })
             .MapWhen(c => c.Request.Path == "/test2", config =>
             {
                 config.Run(async (context) =>
                 {
                     logger.LogInformation("Invoking Test2 endpoint");
                     await context.Response.WriteAsync("HELLO FROM TEST2!");
                 });
             });

            
        }
    }
}
