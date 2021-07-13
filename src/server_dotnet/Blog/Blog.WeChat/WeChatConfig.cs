using Blog.Core.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Blog.WeChat
{
    public class WeChatConfig : ConfigBase<WeChatConfig>
    {
        public string Token { get; set; }

        public string Appid { get; set; }

        public string Secret { get; set; }

        public string EncodingAESKey { get; set; }
    }
}