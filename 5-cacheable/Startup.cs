using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace cacheable
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpCacheHeaders((options) =>
                {
                    options.MaxAge = 3600;
                },
                (options) =>
                {
                    options.AddMustRevalidate = true;
                    options.AddProxyRevalidate = true;
                })
                .AddMvcCore()
                .AddJsonFormatters();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpCacheHeaders();
            app.UseMvc();
        }
    }
}
