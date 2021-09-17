using Blog.Core.Auth;
using Blog.Core.Config;
using Blog.Core.Job;
using Blog.Core.Module.SysRole;
using Blog.Core.Profiles;
using Blog.Core.Setup;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core
{
    public class Startup
    {
        private readonly SwaggerConfig SwaggerConfig = SwaggerConfig.Config;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
            });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(WebApi.WebApiExceptionFilter));
                options.Filters.Add(typeof(WebApi.WebApiContextFilter));
            });

            services.AddHttpContextAccessor();

            // 添加依赖注入服务
            services.AddServices();

            // 添加Jwt认证服务
            services.AddAuthorizationSetup();

            // 添加Swagger
            if (SwaggerConfig.Enable)
                services.AddSwagger();

            // 添加AutoMapper
            services.AddAutoMapper(MapperHelper.MapType());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            app.UseStaticHttpContext();

            app.UseEntityWatcher();

            app.UseJob();

            app.UseSysRole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            if (SwaggerConfig.Enable)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"{SwaggerConfig.Version}/swagger.json", SwaggerConfig.Title);
                    c.RoutePrefix = SwaggerConfig.RoutePrefix;
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "DefaultApi", pattern: "{controller}/{action}/{id}" ).RequireCors("CorsPolicy");
            });
        }
    }
}
