using Blog.Core.Auth;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.Vertification.Mail
{
    public class MailVertificationController : EntityBaseController<mail_vertification, MailVertificationService>
    {
        /// <summary>
        /// 激活用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public string ActivateUser(string id)
        {
            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetSystem());
            return new MailVertificationService().ActivateUser(id);
        }
    }
}
