using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.Common.Config
{
    /// <summary>
    /// 配置基类（appsetting）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseAppConfig<T> where T : class
    {
        public static T Config { get; private set; }
        static BaseAppConfig()
        {
            Config = new JsonConfig("appsettings.json").GetConfig<T>(typeof(T).Name.Replace("Config", ""));
        }
    }
}
