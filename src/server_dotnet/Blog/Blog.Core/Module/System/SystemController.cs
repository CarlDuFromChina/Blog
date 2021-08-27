using Blog.Core.Auth;
using Blog.Core.Config;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using Blog.Core.Utils;
using Blog.Core.WebApi;
using Jdenticon.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.DataService
{
    public class SystemController : BaseApiController
    {
        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public string GetPublicKey()
        {
            return new SystemService().GetPublicKey();
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <returns>图片源</returns>
        [HttpGet, AllowAnonymous]
        public string GetRandomImage()
        {
            return new SystemService().GetRandomImage();
        }

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public object GetAvatar(string id)
        {
            return new SystemService().GetAvatar(id);
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

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public LoginResponse Login(LoginRequest model)
        {
            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetSystem());
            return new SystemService().Login(model);
        }
    }
}