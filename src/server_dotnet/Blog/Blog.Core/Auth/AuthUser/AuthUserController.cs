using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Auth
{
    public class AuthUserController : EntityBaseController<auth_user, AuthUserService>
    {
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize(Policy = "Refresh")]
        public Token RefreshAccessToken()
        {
            var tokenHeader = HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            var user = JwtHelper.SerializeJwt(tokenHeader);
            return JwtHelper.CreateAccessToken(user);
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public void LockUser(string id)
        {
            new AuthUserService().LockUser(id);
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public void UnlockUser(string id)
        {
            new AuthUserService().UnlockUser(id);
        }
    }
}