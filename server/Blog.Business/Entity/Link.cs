using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Blog.Business.Entity
{
    [Entity("link", "链接")]
    public class Link : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember, PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [DataMember, Column, Description("链接地址")]
        public string link_url { get; set; }

        /// <summary>
        /// 链接类型
        /// </summary>
        [DataMember, Column, Description("链接类型")]
        public string link_type { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember, Column, Description("摘要")]
        public string brief { get; set; }
    }
}
