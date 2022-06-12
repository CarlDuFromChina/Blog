using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.SysMenu
{
    public class SysMenuController : EntityBaseController<sys_menu, SysMenuService>
    {
        [HttpGet]
        [Route("GetFirstMenu")]
        public IList<sys_menu> GetFirstMenu()
        {
            return new SysMenuService().GetFirstMenu();
        }
    }
}