
using SixpenceStudio.Platform.Entity;
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


        /// <summary>
        /// 是否前台显示
        /// </summary>
        private int _is_show;
        [DataMember]
        public int is_show
        {
            get
            {
                return this._is_show;
            }
            set
            {
                this._is_show = value;
                SetAttributeValue("is_show", value);
            }
        }


        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdby;
        [DataMember]
        public string createdBy
        {
            get
            {
                return this._createdby;
            }
            set
            {
                this._createdby = value;
                SetAttributeValue("createdBy", value);
            }
        }


        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdbyname;
        [DataMember]
        public string createdByName
        {
            get
            {
                return this._createdbyname;
            }
            set
            {
                this._createdbyname = value;
                SetAttributeValue("createdByName", value);
            }
        }


        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime _createdon;
        [DataMember]
        public DateTime createdOn
        {
            get
            {
                return this._createdon;
            }
            set
            {
                this._createdon = value;
                SetAttributeValue("createdOn", value);
            }
        }


        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedby;
        [DataMember]
        public string modifiedBy
        {
            get
            {
                return this._modifiedby;
            }
            set
            {
                this._modifiedby = value;
                SetAttributeValue("modifiedBy", value);
            }
        }


        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedbyname;
        [DataMember]
        public string modifiedByName
        {
            get
            {
                return this._modifiedbyname;
            }
            set
            {
                this._modifiedbyname = value;
                SetAttributeValue("modifiedByName", value);
            }
        }


        /// <summary>
        /// 修改日期
        /// </summary>
        private DateTime _modifiedon;
        [DataMember]
        public DateTime modifiedOn
        {
            get
            {
                return this._modifiedon;
            }
            set
            {
                this._modifiedon = value;
                SetAttributeValue("modifiedOn", value);
            }
        }


    }
}

