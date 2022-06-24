using Sixpence.ORM.Entity;
using System;
using System.Runtime.Serialization;


namespace Blog.Business.Category
{
    [Entity("category", "博客分类", false)]
    public partial class category : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        [Column]
        public string code { get; set; }

        /// <summary>
        /// 是否付费阅读
        /// </summary>
        [DataMember]
        [Column(defaultValue: true)]
        public bool? is_free { get; set; }

        /// <summary>
        /// 是否付费阅读
        /// </summary>
        [DataMember]
        [Column]
        public string is_free_name { get; set; }

        /// <summary>
        /// 索引
        /// </summary>
        [DataMember]
        [Column]
        public int index { get; set; }
    }
}

