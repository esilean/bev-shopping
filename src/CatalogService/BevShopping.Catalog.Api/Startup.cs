using BevShopping.Catalog.Application.Catalogs;
using BevShopping.Catalog.Data.Data;
using BevShopping.Catalog.Data.Repositories;
using BevShopping.Catalog.Domain.Repositories;
using BevShopping.Shared.Core;
using BevShopping.Shared.Infra.Consul;
using BevShopping.Shared.Infra.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BevShopping.Catalog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
            .AddConsul(Configuration)
            .AddSwagger(Configuration)
            .AddCore(typeof(Create));

            services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            services.AddScoped<CatalogDbContext>();
            services.Configure<CatalogDbConfig>(Configuration.GetSection("DbConfig"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseConsul(lifetime)
                .UseSwagger(Configuration)
                .UseCore();
        }
    }
}
