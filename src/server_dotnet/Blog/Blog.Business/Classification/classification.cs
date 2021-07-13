using Blog.Core.Data;
using System;
using System.Runtime.Serialization;


namespace Blog.Classification
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
        private string _code;
        [DataMember]
        [Attr("code", "编码", AttrType.Varchar, 100)]
        public string code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
                SetAttributeValue("code", value);
            }
        }


        /// <summary>
        /// 是否付费阅读
        /// </summary>
        private int _is_free;
        [DataMember]
        [Attr("is_free", "是否付费阅读", AttrType.Int4)]
        public int is_free
        {
            get
            {
                return this._is_free;
            }
            set
            {
                this._is_free = value;
                SetAttributeValue("is_free", value);
            }
        }

    }
}

