using Microsoft.Extensions.Configuration;
using Sixpence.Common.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.WeChat
{
    public class WeChatApiConfig
    {
        public static IConfiguration Configuration { get; private set; }

        static WeChatApiConfig()
        {
            Configuration = new JsonConfig("wechatApi.json").Configuration;
        }

        public static T GetValue<T>(string keyName)
        {
            return Configuration.GetValue<T>(keyName);
        }

        public static string GetValue(string keyName)
        {
            return GetValue<string>(keyName);
        }
    }
}
