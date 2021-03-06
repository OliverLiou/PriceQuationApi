using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PriceQuationApi.Model;
using Microsoft.EntityFrameworkCore;
using PriceQuationApi.Services;
using PriceQuationApi.Plm;
using Oracle.ManagedDataAccess.Client;

namespace PriceQuationApi
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
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                                       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            services.AddDbContext<PriceQuationContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("PriceQuationContext")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PriceQuationApi", Version = "v1" });
            });
            
            services.AddScoped<IBomService,BomService>();
            services.AddScoped<IOppoService, OppoService>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //跨域
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://localhost:5000","http://localhost:8080","http://localhost:8081")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PriceQuationApi v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
