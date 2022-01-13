using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 博客id
        /// </summary>
        [DataMember]
        [Column("blogid", "博客id", DataType.Varchar, 100)]
        public string blogId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        [Column("content", "内容", DataType.Text)]
        public string content { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        [Column("title", "标题", DataType.Varchar, 100)]
        public string title { get; set; }
    }
}
