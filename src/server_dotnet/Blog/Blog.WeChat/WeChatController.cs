using Blog.Core;
using Blog.Core.Extensions;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
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
        [HttpGet("access_token")]
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
        [HttpGet, HttpPost, AllowAnonymous, Route("Get")]
        public async Task Get()
        {
            if (HttpContext.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                var signature = HttpContext.Request.Query["signature"];
                var timestamp = HttpContext.Request.Query["timestamp"];
                var nonce = HttpContext.Request.Query["nonce"];
                var echostr = HttpContext.Request.Query["echostr"];
                var result = WeChatService.CheckSignature(signature, timestamp, nonce, echostr);
                if (result)
                {
                    await HttpCurrentContext.Response.Write(echostr);
                }
            }
            else
            {
                // 消息接受
                using (StreamReader sr = new StreamReader(HttpContext.Request.Body, Encoding.UTF8))
                {
                    var content = await sr.ReadToEndAsync();
                    var message = WeChatService.ReplyMessage(content);
                    await HttpContext.Response.Write(message);
                }
            }
        }
    }
}