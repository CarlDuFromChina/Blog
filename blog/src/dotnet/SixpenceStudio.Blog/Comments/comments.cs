using Newtonsoft.Json.Linq;
using SixpenceStudio.Platform.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.Blog.Comments
{
    [EntityName("comments")]
    public class comments : BaseEntity
    {
        private string _commentsid;
        [DataMember]
        public string commentsId
        {
            get
            {
                return this._commentsid;
            }
            set
            {
                this._commentsid = value;
                SetAttributeValue("commentsId", value);
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
        private DateTime? _createdon;
        [DataMember]
        public DateTime? createdOn
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
        /// 创建日期
        /// </summary>
        private DateTime? _modifiedon;
        [DataMember]
        public DateTime? modifiedOn
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

        /// <summary>
        /// 评论
        /// </summary>
        private string _comment;
        [DataMember]
        public string comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
                SetAttributeValue("comment", value);
            }
        }

        /// <summary>
        /// 头像
        /// </summary>
        private string _avatar;
        [DataMember]
        public string avatar
        {
            get
            {
                return this._avatar;
            }
            set
            {
                this._avatar = value;
                SetAttributeValue("avatar", value);
            }
        }

        /// <summary>
        /// 实体Id
        /// </summary>
        private string _objectid;
        [DataMember]
        public string objectId
        {
            get
            {
                return this._objectid;
            }
            set
            {
                this._objectid = value;
                SetAttributeValue("objectId", value);
            }
        }
    }
}
