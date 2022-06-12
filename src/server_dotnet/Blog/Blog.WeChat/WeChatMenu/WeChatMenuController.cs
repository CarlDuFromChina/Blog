using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.WeChatMenu
{
    public class WeChatMenuController : BaseApiController
    {
        [HttpPost, Route("CreateMenu")]
        public void CreateMenu(SelfMenuInfo menu)
        {
            WeChatMenuService.CreateMenu(menu);
        }

        [HttpGet, Route("GetMenu")]
        public WeChatMenuModel GetMenu()
        {
            return WeChatMenuService.GetMenu();
        }

        [HttpGet, Route("DeleteMenu")]
        public void DeleteMenu()
        {
            WeChatMenuService.DeleteMenu();
        }
    }
}
