using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Post.Sync.Juejin
{
    public class JuejinCategory
    {
        public string category_id { get; set; }
        public JuejinCategoryData category { get; set; }
        public JuejinUserInteract user_interact { get; set; }
        public List<JuejinTag> hot_tags { get; set; }
    }

    public class JuejinCategoryData
    {
        public string category_id { get; set; }
        public string category_name { get; set; }
        public string category_url { get; set; }
        public int rank { get; set; }
        public string back_ground { get; set; }
        public string icon { get; set; }
        public int ctime { get; set; }
        public int mtime { get; set; }
        public int show_type { get; set; }
        public int item_type { get; set; }
        public int promote_tag_cap { get; set; }
        public int promote_priority { get; set; }
    }
}
