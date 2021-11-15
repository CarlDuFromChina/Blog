using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Blog
{
    public partial class blog
    {
        /// <summary>
        /// 点赞数
        /// </summary>
        [DataMember]
        public int upvote_times { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        [DataMember]
        public int comment_count { get; set; }
    }

    public class BlogActivityModel
    {
        public string created_date { get; set; }
        public int? count { get; set; }
    }
}
