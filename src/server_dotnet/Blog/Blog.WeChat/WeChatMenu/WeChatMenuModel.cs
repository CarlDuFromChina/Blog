using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.WeChatMenu
{
    public class WeChatMenuModel
    {
        public int is_menu_open { get; set; }
        public SelfMenuInfo selfmenu_info { get; set; }
    }

    public class SelfMenuInfo
    {
        public List<WeChatMenuButtonModel> button { get; set; }
    }
    public class WeChatMenuButtonModel
    {
        public string type { get; set; }
        public string name { get; set; }

        /// <summary>
        ///类型为click必须要key
        /// </summary>
        public string key { get; set; }
        public string url { get; set; }

        public string value { get; set; }

        public List<WeChatMenuButtonModel> sub_button { get; set; }

        public NewsInfo news_info { get; set; }
    }

    public class NewsInfo
    {
        public List<News> list { get; set; }
    }

    public class News
    {
        public string title { get; set; }
        public string author { get; set; }
        public string digest { get; set; }
        public int show_cover { get; set; }
        public string cover_url { get; set; }
        public string content_url { get; set; }
        public string source_url { get; set; }
    }
}
