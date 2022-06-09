using Blog.Core.Auth;
using Blog.Core.Job;
using Blog.Core.Module.SysRole;
using Blog.Core.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Sixpence.Common;
using Sixpence.ORM.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
               .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);

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
            services.AddServiceContainer(options =>
            {
                options.Assembly.Add("Blog.*.dll");
            });

            // 添加Jwt认证服务
            services.AddJwt();

            // 添加Swagger
#if DEBUG
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "接口文档", Version = "v1" });
            });
#endif

            // 添加AutoMapper
            services.AddAutoMapper(MapperHelper.MapType());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            app.UseStaticHttpContext();

            app.UseORM(options =>
            {
                options.EntityClassNameCase = NameCase.UnderScore;
                options.AutoGenerate = true;
            });

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

#if DEBUG
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"v1/swagger.json", "接口文档");
                c.RoutePrefix = "Swagger";
            });
#endif

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "DefaultApi", pattern: "{controller}/{action}/{id}" ).RequireCors("CorsPolicy");
            });
        }
    }
}
