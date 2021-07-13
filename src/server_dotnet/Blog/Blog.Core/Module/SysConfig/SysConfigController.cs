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
        [HttpGet, AllowAnonymous]
        public object GetValue(string code)
        {
            return new SysConfigService().GetValue(code);
        }
    }
}