using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [PrimaryColumn(primaryType: PrimaryType.GUIDNumber)]
        public string id { get; set; }

        /// <summary>
        /// 博客id
        /// </summary>
        [DataMember, Column, Description("博客id")]
        public string postid { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember, Column, Description("标题")]
        public string content { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember, Column, Description("标题")]
        public string title { get; set; }
    }
}
