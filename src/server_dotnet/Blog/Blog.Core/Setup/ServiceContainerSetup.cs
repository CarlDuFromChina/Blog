using Blog.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Core.Setup
{
    public static class ServiceContainerSetup
    {
        public static void AddServices(this IServiceCollection services)
        {
            ServiceContainer.Services = services;
            var types = AssemblyUtil.GetAssemblies().GetTypes();
            var interfaces = types.Where(item => item.IsInterface && item.IsDefined(typeof(ServiceRegisterAttribute), false)).ToList();
            interfaces.ForEach(item =>
            {
                var _types = types.Where(type => !type.IsInterface && !type.IsAbstract && type.GetInterfaces().Contains(item) && !type.IsDefined(typeof(IgnoreServiceRegisterAttribute), false)).ToList();
                _types.ForEach(type => ServiceContainer.Register(item, type));
            });
        }
    }
}
