using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Config
{
    public class BaseConfig<T> where T : class
    {
        public static T Config { get; private set; }
        static BaseConfig()
        {
            Config = AppConfig.GetConfig<T>(typeof(T).Name.Replace("Config", ""));
        }
    }
}
