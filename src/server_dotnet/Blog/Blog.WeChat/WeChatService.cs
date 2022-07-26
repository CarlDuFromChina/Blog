using Blog.Core.WebApi;
using Blog.WeChat.Message;
using Blog.WeChat.Message.Text;
using Blog.WeChat.WeChatReply.Focus;
using Blog.WeChat.WeChatReply.Keywords;
using System;
using System.IO;
using System.Text;
using System.Xml;
using Sixpence.Common.Utils;
using Blog.Core;
using Sixpence.Common.Logging;
using Sixpence.Common;

namespace Blog.WeChat
{
    public static class WeChatService
    {
        private static string _appid;
        private static string _token;
        private static string _secret;
        private static string _encodingAESKey;
        private static readonly object lockObject = new Object();
        /// <summary>
        /// 获取access_token
        /// </summary>
        public static string AccessToken
        {
            get
            {
                var accessToken = MemoryCacheUtil.GetCacheItem<AccessTokenResponse>("AccessToken");
                if (accessToken == null)
                {
                    lock (lockObject)
                    {
                        if (accessToken == null)
                        {
                            accessToken = RefreshToken();
                        }
                    }
                }
                return accessToken.AccessToken;
            }
        }



        static WeChatService()
        {
            var config = WeChatConfig.Config;
            AssertUtil.IsNull(config, "未找到微信公众号配置");
            _appid = config.Appid;
            _token = config.Token;
            _secret = config.Secret;
            _encodingAESKey = config.EncodingAESKey;
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        public static AccessTokenResponse RefreshToken()
        {
            var result = WeChatApi.GetAccessToken(_appid, _secret);
            var accessToken = new AccessTokenResponse()
            {
                AccessToken = result.AccessToken,
                Expire = result.Expire
            };
            MemoryCacheUtil.RemoveCacheItem("AccessToken");
            MemoryCacheUtil.Set("AccessToken", accessToken);
            var logger = LoggerFactory.GetLogger("wechat");
            logger.Debug("获取微信access_token成功：" + accessToken.AccessToken);
            return accessToken;
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public static bool CheckSignature(string signature, string timestamp, string nonce, string echostr)
        {
            string[] arrTmp = { _token, timestamp, nonce };
            Array.Sort(arrTmp);
            var tmpStr = string.Join("", arrTmp);
            tmpStr = SHAUtil.GetSwcSHA1(tmpStr).ToLower();
            return tmpStr == signature;
        }

        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReplyMessage(string content)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(content);

            switch (xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText)
            {
                case "text":
                    return new WeChatKeywordsService().GetReplyMessage(new WeChatTextMessage(xml));
                case "event":
                    return new WeChatFocusReplyService().GetReplyMessage(new BaseWeChatMessage(xml));
                default:
                    return "success";
            }
        }
    }
}