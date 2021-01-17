using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using RhipeApi.BL;
using RhipeApi.Infrastructure;
using RhipeApi.Service;
using RhipeApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RhipeApi
{
    public class Startup
    {
        private readonly string localApis = "_localApis";
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddHttpClientServices(Configuration);
            services.AddControllers();

            #region Swagger
            services.AddSwaggerGen();
            #endregion

            services.AddAutoMapper(typeof(Startup));

            #region DI
            services.AddTransient<IProductVM, ProductVM>();
            services.AddTransient<IProductBL, ProductBL>();
            #endregion

            //services.AddCors(o => o.AddPolicy(localApis, builder =>
            //{
            //    builder.WithOrigins("url1",
            //            "url-production",
            //            "https://localhost:44343")
            //        .AllowAnyMethod()
            //        .AllowAnyHeader();


            //}));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(localApis);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                #region Add Swagger only in Development mode
                
                app.UseSwagger();
                
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rhipe API");
                });
                #endregion
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
