using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace middleware
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureLogging(factory=>{
                    factory.AddConsole();
                })
                .Configure(builder =>
                {
                    builder
                        .UseMiddleware<Middleware1>()
                        .UseMiddleware<Middleware2>()
                        .UseMiddleware<Middleware3>()
                        .Run(async app=>await app.Response.WriteAsync("Awesome!"));
                    
                })
                .Build();

            host.Run();

        }
    }
}
