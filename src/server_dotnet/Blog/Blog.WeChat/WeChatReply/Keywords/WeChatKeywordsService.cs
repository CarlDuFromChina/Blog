#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/1 20:30:09
Description：微信关键词回复 Service
********************************************************/
#endregion

using Blog.Core.WebApi;
using Sixpence.EntityFramework.Entity;
using Blog.WeChat.Message;
using Blog.WeChat.Message.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.EntityFramework.Broker;

namespace Blog.WeChat.WeChatReply.Keywords
{
    public class WeChatKeywordsService : EntityService<wechat_keywords>
    {
        #region 构造函数
        public WeChatKeywordsService()
        {
            this._context = new EntityContext<wechat_keywords>();
        }

        public WeChatKeywordsService(IPersistBroker broker)
        {
            this._context = new EntityContext<wechat_keywords>(broker);
        }
        #endregion

        /// <summary>
        /// 根据关键词获取回复消息
        /// </summary>
        /// <param name="requestMesage"></param>
        /// <returns></returns>
        public IEnumerable<wechat_keywords> GetDataList(string requestMesage)
        {
            var sql = @"
SELECT * FROM wechat_keywords
WHERE name LIKE CONCAT('%', @name, '%')
";
            return Broker.RetrieveMultiple<wechat_keywords>(sql, new Dictionary<string, object>() { { "@name", requestMesage } });
        }

        /// <summary>
        /// 关键词回复
        /// </summary>
        /// <param name="requestMsg"></param>
        /// <returns></returns>
        public string GetReplyMessage(WeChatTextMessage message)
        {
            var responseMsg = GetDataList(message.Content)?.FirstOrDefault()?.reply_content;
            if (string.IsNullOrEmpty(responseMsg))
            {
                responseMsg = "对不起，我听不懂你在说什么";
            }

            return string.Format(WeChatMessageTemplate.Text, message.FromUserName, message.ToUserName, DateTime.Now.Ticks, responseMsg);
        }
    }
}
