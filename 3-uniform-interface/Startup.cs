using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using uniform_interface.Formatters;
using uniform_interface.Models;

namespace uniform_interface
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("profiles.json", optional: false, reloadOnChange: true);

                
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .Configure<List<Profile>>(Configuration.GetSection("profiles"))
                .AddMvcCore(o=>{
                    o.OutputFormatters.Add(new CsvOutputFormatter());
                })
                .AddJsonFormatters()
                .AddJsonOptions(o=>{
                    o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    o.SerializerSettings.Formatting = Formatting.Indented;
                 });
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
