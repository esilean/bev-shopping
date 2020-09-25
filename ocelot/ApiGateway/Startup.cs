using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System.IO;

namespace ApiGateway
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var ocelotJson = new JObject();
            foreach (var jsonFilename in Directory.EnumerateFiles("Configuration", "ocelot.*.json", SearchOption.AllDirectories))
            {
                using StreamReader fi = File.OpenText(jsonFilename);
                var json = JObject.Parse(fi.ReadToEnd());
                ocelotJson.Merge(json, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                });
            }

            File.WriteAllText("ocelot.json", ocelotJson.ToString());

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"ocelot.{env.EnvironmentName}.json",
                    optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                    optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOcelot(Configuration)
                .AddConsul()
                .AddConfigStoredInConsul();

            services.AddSwaggerForOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });

            app.UseOcelot().Wait();
        }
    }
}
