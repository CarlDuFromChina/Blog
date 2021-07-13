using Blog.Core.Extensions;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Blog.WeChat.WeChatMaterialExtension;

namespace Blog.WeChat
{
    public class WeChatController : BaseApiController
    {
        /// <summary>
        /// 获取微信access_token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetAccessToken()
        {
            return WeChatService.AccessToken;
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        [HttpGet, HttpPost, AllowAnonymous]
        public async Task Get()
        {
            if (Core.HttpContext.Current.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                var signature = Core.HttpContext.Current.Request.Query["signature"];
                var timestamp = Core.HttpContext.Current.Request.Query["timestamp"];
                var nonce = Core.HttpContext.Current.Request.Query["nonce"];
                var echostr = Core.HttpContext.Current.Request.Query["echostr"];
                var result = WeChatService.CheckSignature(signature, timestamp, nonce, echostr);
                if (result)
                {
                    await Core.HttpContext.Current.Response.Write(echostr);
                }
            }
            else
            {
                // 消息接受
                var message = WeChatService.ReplyMessage(Core.HttpContext.Current.Request.Body);
                await Core.HttpContext.Current.Response.Write(message);
            }
        }
    }
}