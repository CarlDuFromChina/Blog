using log4net;
using Blog.Core.WebApi;
using Sixpence.ORM.Entity;
using Blog.WeChat.Message;
using System;
using System.Collections.Generic;
using Sixpence.Common.Logging;
using Sixpence.ORM.EntityManager;

namespace Blog.WeChat.WeChatReply.Focus
{
    public class WeChatFocusReplyService : EntityService<wechat_focus_reply>
    {
        #region 构造函数
        public WeChatFocusReplyService() : base() { }
        public WeChatFocusReplyService(IEntityManager manager) : base(manager) { }
        #endregion

        private ILog logger = LoggerFactory.GetLogger("wechat");

        public void Activate(string id)
        {
            var data = GetData(id);
            data.@checked = true;
            UpdateData(data);
        }

        public void Deactivate(string id)
        {
            var data = GetData(id);
            data.@checked = false;
            UpdateData(data);
        }

        public wechat_focus_reply GetData()
        {
            var config = WeChatConfig.Config;
            var sql = @"
select * from wechat_focus_reply where wechat = @wechat
";
            var data = Manager.QueryFirst<wechat_focus_reply>(sql, new Dictionary<string, object>() { { "@wechat", config.Appid } });
            return data;
        }

        /// <summary>
        /// 获取事件回复
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string GetReplyMessage(BaseWeChatMessage message)
        {
            if (!string.IsNullOrEmpty(message.EventName) && message.EventName.Trim() == "subscribe")
            {
                var reply = GetData()?.content ?? "感谢您的关注！";
                logger.Info($"收到来自{message.FromUserName}的关注");
                return string.Format(WeChatMessageTemplate.Text, message.FromUserName, message.ToUserName, DateTime.Now.Ticks, reply);
            }
            return "";
        }
    }
}
