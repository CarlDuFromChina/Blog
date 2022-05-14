using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.SysConfig
{
    public class SysConfigController : EntityBaseController<sys_config, SysConfigService>
    {
        [HttpGet]
        public object GetValue(string code)
        {
            return new SysConfigService().GetValue(code);
        }

        /// <summary>
        /// 是否开启评论
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public string EnableComment()
        {
            return new SysConfigService().GetValue("enable_comment")?.ToString();
        }

        /// <summary>
        /// 网站信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public string WebsiteInfo()
        {
            return new SysConfigService().GetValue("website_info")?.ToString();
        }
    }
}