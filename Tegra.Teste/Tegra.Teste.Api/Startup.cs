using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Tegra.Teste.Application.Application;
using Tegra.Teste.Application.Application.Interface;
using Tegra.Teste.Infra.Infra;
using Tegra.Teste.Infra.Infra.Interface;
using Tegra.Teste.Infra.Repository;
using Tegra.Teste.Infra.Repository.Interface;

namespace Tegra.Teste.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IUtil, Util>();
            services.AddTransient<IAeroportoRepository, AeroportoRepository>();
            services.AddTransient<IVooRepository, VooRepository>();
            services.AddTransient<IVooApplication, VooApplication>();
            services.AddTransient<IAeroportoApplication, AeroportoApplication>();

            services.AddCors(options =>
            {
                options.AddPolicy("AlloSpecificOrigin",
                    builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Teste Tegra", Version = "v1" });
            });

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste Tegra");

            });

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("AlloSpecificOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
