using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Post.Sync.Juejin
{
    public class JuejinUserInteract
    {
        public long id { get; set; }
        public int omitempty { get; set; }
        public long user_id { get; set; }
        public string is_digg { get; set; }
        public string is_follow { get; set; }
        public string is_collect { get; set; }
    }
}
