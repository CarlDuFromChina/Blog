using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.Core.Config
{
    public class ConfigBase<T> where T : class
    {
        public static T Config { get; private set; }
        static ConfigBase()
        {
            Config = AppConfig.GetConfig<T>(typeof(T).Name.Replace("Config", ""));
        }
    }
}
