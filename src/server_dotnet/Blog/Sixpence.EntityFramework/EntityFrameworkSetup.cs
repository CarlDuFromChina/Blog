using Microsoft.Extensions.DependencyInjection;
using Sixpence.Core;
using Sixpence.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixpence.EntityFramework
{
    public static class EntityFrameworkSetup
    {
        public static void AddServices(this IServiceCollection services, Action<ServiceOptions> action = null)
        {
            ServiceContainer.Services = services;
            var options = new ServiceOptions() { Assembly = new List<string>() { "Sixpence.*.dll" } };
            if (action != null)
            {
                action.Invoke(options);
            }

            var types = AssemblyUtil.GetAssemblies(options.Assembly?.ToArray()).GetTypes();
            var interfaces = types.Where(item => item.IsInterface && item.IsDefined(typeof(ServiceRegisterAttribute), false)).ToList();
            interfaces.ForEach(item =>
            {
                var _types = types.Where(type => !type.IsInterface && !type.IsAbstract && type.GetInterfaces().Contains(item) && !type.IsDefined(typeof(IgnoreServiceRegisterAttribute), false)).ToList();
                _types.ForEach(type => ServiceContainer.Register(item, type));
            });
        }

        public class ServiceOptions
        {
            public List<string> Assembly { get; set; }
        }
    }
}
