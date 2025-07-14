using Sixpence.ORM.Entity;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Blog.Business.Entity
{
    [Entity("category", "博客分类")]
    public partial class Category : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember, PrimaryColumn, Description("id")]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DataMember, Column, Description("编码")]
        public string code { get; set; }

        /// <summary>
        /// 是否付费阅读
        /// </summary>
        [DataMember, Column(defaultValue: true), Description("是否付费阅读")]
        public bool? is_free { get; set; }

        /// <summary>
        /// 是否付费阅读
        /// </summary>
        [DataMember, Column, Description("是否付费阅读")]
        public string is_free_name { get; set; }

        /// <summary>
        /// 索引
        /// </summary>
        [DataMember, Column, Description("索引")]
        public int index { get; set; }
    }
}

