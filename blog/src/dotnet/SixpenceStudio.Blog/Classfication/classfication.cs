
using SixpenceStudio.Platform.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.Blog.Classfication
{
    [EntityName("classfication")]
    public partial class classfication : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _classficationid;
        [DataMember]
        public string classficationId
        {
            get
            {
                return this._classficationid;
            }
            set
            {
                this._classficationid = value;
                SetAttributeValue("classficationId", value);
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
        public string createdby
        {
            get
            {
                return this._createdby;
            }
            set
            {
                this._createdby = value;
                SetAttributeValue("createdby", value);
            }
        }


        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdbyname;
        [DataMember]
        public string createdbyname
        {
            get
            {
                return this._createdbyname;
            }
            set
            {
                this._createdbyname = value;
                SetAttributeValue("createdbyname", value);
            }
        }


        /// <summary>
        /// 创建日期
        /// </summary>
        private string _createdon;
        [DataMember]
        public string createdon
        {
            get
            {
                return this._createdon;
            }
            set
            {
                this._createdon = value;
                SetAttributeValue("createdon", value);
            }
        }


        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedby;
        [DataMember]
        public string modifiedby
        {
            get
            {
                return this._modifiedby;
            }
            set
            {
                this._modifiedby = value;
                SetAttributeValue("modifiedby", value);
            }
        }


        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedbyname;
        [DataMember]
        public string modifiedbyname
        {
            get
            {
                return this._modifiedbyname;
            }
            set
            {
                this._modifiedbyname = value;
                SetAttributeValue("modifiedbyname", value);
            }
        }


        /// <summary>
        /// 修改日期
        /// </summary>
        private string _modifiedon;
        [DataMember]
        public string modifiedon
        {
            get
            {
                return this._modifiedon;
            }
            set
            {
                this._modifiedon = value;
                SetAttributeValue("modifiedon", value);
            }
        }


    }
}

