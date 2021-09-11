using Microsoft.Extensions.Configuration;
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
            //构建Configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("wechatApi.json");
            Configuration = builder.Build();
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
