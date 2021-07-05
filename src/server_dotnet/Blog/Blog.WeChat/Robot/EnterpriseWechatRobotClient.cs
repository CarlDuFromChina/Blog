using Newtonsoft.Json;
using Blog.WeChat.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Utils;

namespace Blog.WeChat.Robot
{
    public class EnterpriseWechatRobotClient : IRobotClient
    {
        public EnterpriseWechatRobotClient(string hook)
        {
            this.hook = hook;
        }
        private string hook { get; set; }

        private class message
        {
            public string content { get; set; }
        }
        private class text_message
        {
            public string msgtype { get; set; }
            public message text { get; set; }
        }

        public void SendTextMessage(string text)
        {
            var data = JsonConvert.SerializeObject(new text_message
            {
                msgtype = "text",
                text = new message()
                {
                    content = text
                }
            });
            HttpUtil.Post(hook, data);
        }
    }
}

