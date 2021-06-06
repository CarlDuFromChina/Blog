
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.Blog.Classification
{
    [EntityName("classification")]
    public partial class classification : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _classificationid;
        [DataMember]
        public string classificationId
        {
            get
            {
                return this._classificationid;
            }
            set
            {
                this._classificationid = value;
                SetAttributeValue("classificationId", value);
            }
        }


        /// <summary>
        /// 编码
        /// </summary>
        private string _code;
        [DataMember]
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

