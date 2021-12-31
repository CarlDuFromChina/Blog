using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.Common.Config
{
    public class JsonConfig
    {
        public IConfiguration Configuration { get; private set; }

        public JsonConfig(string configName)
        {
            //构建Configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(configName);
            Configuration = builder.Build();
        }

        public T GetValue<T>(string keyName)
        {
            return Configuration.GetValue<T>(keyName);
        }

        public T GetConfig<T>(string keyName) where T : class
        {
            return Configuration.GetSection(keyName).Get<T>();
        }
    }
}
