using Blog.Core.Auth.Role;
using Blog.Core.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core
{
    public static class AuthorizationSetup
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Api", policy => policy.RequireClaim("Type", "AccessToken").Build());
                options.AddPolicy("Refresh", policy => policy.RequireClaim("Type", "RefreshToken").Build());
            });

            // 读取配置文件
            var symmetricKeyAsBase64 = JwtConfig.Config.SecretKey;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var Issuer = JwtConfig.Config.Issuer;
            var Audience = JwtConfig.Config.Audience;

            //2.1【认证】、core自带官方JWT认证
            // 开启Bearer认证
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             // 添加JwtBearer服务
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = signingKey,
                     ValidateIssuer = true,
                     ValidIssuer = Issuer, // 发行人
                     ValidateAudience = true,
                     ValidAudience = Audience,//订阅人
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.FromSeconds(30),
                     RequireExpirationTime = true,
                 };
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.StatusCode = 403;
                             context.Response.ContentType = "application/json";
                             var error = "Token过期";
                             return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                         }
                         return Task.CompletedTask;
                     }
                 };
             });
        }
    }
}
