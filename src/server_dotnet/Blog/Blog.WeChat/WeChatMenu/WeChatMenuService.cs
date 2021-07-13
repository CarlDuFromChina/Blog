using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.WeChatMenu
{
    public static class WeChatMenuService
    {
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="menu"></param>
        public static void CreateMenu(SelfMenuInfo menu)
        {
            WeChatApi.CreateMenu(JsonConvert.SerializeObject(menu));
        }

        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <returns></returns>
        public static WeChatMenuModel GetMenu()
        {
            var resp = WeChatApi.GetMenu();
            return JsonConvert.DeserializeObject<WeChatMenuModel>(resp);
        }

        /// <summary>
        /// 删除全部菜单
        /// </summary>
        public static void DeleteMenu()
        {
            WeChatApi.DeleteMenu();
        }
    }
}
