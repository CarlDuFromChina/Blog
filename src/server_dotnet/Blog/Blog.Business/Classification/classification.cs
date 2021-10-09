using Sixpence.EntityFramework.Entity;
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
        [Attr("classificationid", "博客分类id", AttrType.Varchar, 100)]
        public string classificationId
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
        /// 编码
        /// </summary>
        [DataMember]
        [Attr("code", "编码", AttrType.Varchar, 100)]
        public string code { get; set; }

        /// <summary>
        /// 是否付费阅读
        /// </summary>
        [DataMember]
        [Attr("is_free", "是否付费阅读", AttrType.Int4)]
        public int is_free { get; set; }

        /// <summary>
        /// 索引
        /// </summary>
        [DataMember]
        [Attr("index", "索引", AttrType.Int4)]
        public int index { get; set; }
    }
}

