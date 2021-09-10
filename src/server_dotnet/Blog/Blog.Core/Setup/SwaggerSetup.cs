using Blog.Core.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Setup
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            var config = SwaggerConfig.Config;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(config.Version, new OpenApiInfo { Title = config.Title, Version = config.Version });
            });
        }
    }
}
