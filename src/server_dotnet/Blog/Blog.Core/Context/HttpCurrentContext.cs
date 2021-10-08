using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core
{
    public class HttpCurrentContext
    {
        private static IHttpContextAccessor _accessor;
        public static HttpContext HttpContext => _accessor.HttpContext;
        public static HttpRequest Request => _accessor.HttpContext.Request;
        public static HttpResponse Response => _accessor.HttpContext.Response;
        internal static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
    }

    public static class StaticHttpContextExtensions
    {
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpCurrentContext.Configure(httpContextAccessor);
            return app;
        }
    }
}
