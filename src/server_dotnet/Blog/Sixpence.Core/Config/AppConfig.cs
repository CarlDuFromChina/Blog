using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.Core.Config
{
    public static class AppConfig
    {
        public static IConfiguration Configuration { get; private set; }

        static AppConfig()
        {
            //构建Configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public static T GetValue<T>(string keyName)
        {
            return Configuration.GetValue<T>(keyName);
        }

        public static T GetConfig<T>(string keyName) where T : class
        {
            return Configuration.GetSection(keyName).Get<T>();
        }
    }
}
