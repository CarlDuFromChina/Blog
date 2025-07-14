using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Sixpence.Common;
using Sixpence.ORM.Extensions;
using Sixpence.Web;
using Sixpence.Web.Auth;
using Sixpence.Web.Extensions;
using Sixpence.Web.Job;
using Sixpence.Web.Profiles;
using Sixpence.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
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

            services
                .AddControllers(options =>
                {
                    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
                });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(WebApiExceptionFilter));
                options.Filters.Add(typeof(WebApiContextFilter));
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            app.UseStaticHttpContext();

            app.UseORM(options =>
            {
                options.EntityClassNameCase = NameCase.Pascal;
                options.AutoGenerate = true;
            });

            app.UseJob();

            app.UseSysRole();

            app.UseSysConfig();

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
                endpoints.MapControllerRoute
                (
                    name: "default",
                    pattern: "{controller:slugify}/{action:slugify}/{id?}"
                )
                .RequireCors("CorsPolicy");
            });
        }
    }
}
