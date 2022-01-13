using Sixpence.ORM.Entity;
using System;
using System.Runtime.Serialization;


namespace Blog.Business.Classification
{
    [Entity("classification", "博客分类", false)]
    public partial class classification : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        [Column("code", "编码", DataType.Varchar, 100)]
        public string code { get; set; }

        /// <summary>
        /// 是否付费阅读
        /// </summary>
        [DataMember]
        [Column("is_free", "是否付费阅读", DataType.Int4)]
        public int is_free { get; set; }

        /// <summary>
        /// 索引
        /// </summary>
        [DataMember]
        [Column("index", "索引", DataType.Int4)]
        public int index { get; set; }
    }
}

