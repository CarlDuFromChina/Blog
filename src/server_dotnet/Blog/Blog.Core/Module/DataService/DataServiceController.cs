using Blog.Core.Auth;
using Blog.Core.Config;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using Blog.Core.Utils;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.DataService
{
    public class DataServiceController : BaseApiController
    {
        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public string GetPublicKey()
        {
            return new DataService().GetPublicKey();
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <returns>图片源</returns>
        [HttpGet, AllowAnonymous]
        public string GetRandomImage()
        {
            return new DataService().GetRandomImage();
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public bool Test()
        {
            var token = Core.HttpContext.Current.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            else
            {
                return JwtHelper.SerializeJwt(token) != null;
            }
        }
    }
}