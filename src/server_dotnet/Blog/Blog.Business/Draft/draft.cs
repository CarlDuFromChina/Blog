using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Draft
{
    [Entity("draft", "草稿", false)]
    public partial class draft : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        [Attr("draftid", "草稿id", AttrType.Varchar, 100)]
        public string draftId
        {
            get
            {
                return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }

        /// <summary>
        /// 博客id
        /// </summary>
        [DataMember]
        [Attr("blogid", "博客id", AttrType.Varchar, 100)]
        public string blogId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        [Attr("content", "内容", AttrType.Text)]
        public string content { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        [Attr("title", "标题", AttrType.Varchar, 100)]
        public string title { get; set; }
    }
}
