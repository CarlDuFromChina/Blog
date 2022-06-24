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
        [HttpPost]
        public void CreateMenu(SelfMenuInfo menu)
        {
            WeChatMenuService.CreateMenu(menu);
        }

        [HttpGet]
        public WeChatMenuModel GetMenu()
        {
            return WeChatMenuService.GetMenu();
        }

        [HttpDelete]
        public void DeleteMenu()
        {
            WeChatMenuService.DeleteMenu();
        }
    }
}
