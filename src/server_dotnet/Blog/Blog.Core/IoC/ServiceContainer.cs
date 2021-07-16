using Blog.Core.Auth;
using Blog.Core.Job;
using Blog.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core
{
    public static class ServiceContainer
    {
        private static IServiceCollection Services;

        public static void AddServices(IServiceCollection services)
        {
            Services = services;

            var types = AssemblyUtil.GetAssemblies().GetTypes();
            var interfaces = types.Where(item => item.IsInterface && item.IsDefined(typeof(ServiceRegisterAttribute), false)).ToList();
            interfaces.ForEach(item =>
            {
                var _types = types.Where(type => !type.IsInterface && !type.IsAbstract && type.GetInterfaces().Contains(item) && !type.IsDefined(typeof(IgnoreServiceRegisterAttribute), false)).ToList();
                _types.ForEach(type => Register(item, type));
            });
        }

        public static void Register(Type typeFrom, Type typeTo)
        {
            Services.AddTransient(typeFrom, typeTo);
        }

        public static T Resolve<T>()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetServices<T>().FirstOrDefault();
        }

        public static T Resolve<T>(string name)
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetServices<T>().FirstOrDefault(item => item.GetType().Name.ToLower().Contains(name.ToLower()));
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetServices<T>();
        }

        public static IEnumerable<object> ResolveAll(Type type)
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetServices(type);
        }

        public static T Resolve<T>(Func<string, bool> action)
        {
            var list = ResolveAll<T>();
            return list.Where(item => action(item.GetType().Name)).FirstOrDefault();
        }


        public static IEnumerable<T> ResolveAll<T>(Func<string, bool> func)
        {
            return ResolveAll<T>().Where(item => func(item.GetType().Name));
        }
    }
}
