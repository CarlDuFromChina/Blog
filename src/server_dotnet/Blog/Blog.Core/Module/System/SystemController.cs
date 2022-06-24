using Blog.Core.Auth;
using Blog.Core.Config;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using Sixpence.Common.Utils;
using Blog.Core.WebApi;
using Jdenticon.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Core.Module.System.Models;

namespace Blog.Core.Module.DataService
{
    public class SystemController : BaseApiController
    {
        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        [HttpGet("public_key"), AllowAnonymous]
        public string GetPublicKey()
        {
            return new SystemService().GetPublicKey();
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <returns>图片源</returns>
        [HttpGet("random_image"), AllowAnonymous]
        public string GetRandomImage()
        {
            return new SystemService().GetRandomImage();
        }

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("avatar/{id}"), AllowAnonymous]
        public object GetAvatar(string id)
        {
            return new SystemService().GetAvatar(id);
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("test"), AllowAnonymous]
        public bool Test()
        {
            var token = HttpCurrentContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
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
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login"), AllowAnonymous]
        public LoginResponse Login(LoginRequest model)
        {
            return new SystemService().Login(model);
        }

        /// <summary>
        /// 登录参数
        /// </summary>
        /// <returns></returns>
        [HttpGet("login_config"), AllowAnonymous]
        public LoginConfig LoginConfig()
        {
            return new SystemService().GetLoginConfig();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("signup"), AllowAnonymous]
        public LoginResponse Signup(LoginRequest model)
        {
            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetSystem());
            return new SystemService().Signup(model);
        }

        /// <summary>
        /// 登录或注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("fast_signin"), AllowAnonymous]
        public LoginResponse SignInOrSignUp(LoginRequest model)
        {
            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetSystem());
            return new SystemService().SignInOrSignUp(model);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        [HttpPut("password")]
        public void EditPassword([FromBody] string password)
        {
            new SystemService().EditPassword(password);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("password/reset")]
        public void ResetPassword(string id)
        {
            new SystemService().ResetPassword(id);
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="code"></param>
        [HttpGet("password/forget")]
        public void ForgetPassword(string code)
        {
            new SystemService().ForgetPassword(code);
        }

        /// <summary>
        /// 是否展示后台
        /// </summary>
        /// <returns></returns>
        [HttpGet("is_show_admin")]
        public bool GetShowAdmin()
        {
            return new SystemService().GetShowAdmin();
        }
    }
}