
using Newtonsoft.Json.Linq;
using SixpenceStudio.Platform.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.Blog.WeChatNews
{
    [EntityName("wechat_news")]
    public partial class wechat_news : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _wechat_newsid;
        [DataMember]
        public string wechat_newsId
        {
            get
            {
                return this._wechat_newsid;
            }
            set
            {
                this._wechat_newsid = value;
                SetAttributeValue("wechat_newsId", value);
            }
        }

        
        /// <summary>
        /// 内容
        /// </summary>
        private JToken _content;
        [DataMember]
        public JToken content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
                SetAttributeValue("content", value);
            }
        }


        /// <summary>
        /// 媒体id
        /// </summary>
        private string _media_id;
        [DataMember]
        public string media_id
        {
            get
            {
                return this._media_id;
            }
            set
            {
                this._media_id = value;
                SetAttributeValue("media_id", value);
            }
        }


        /// <summary>
        /// 上传时间
        /// </summary>
        private long? _update_time;
        [DataMember]
        public long? update_time
        {
            get
            {
                return this._update_time;
            }
            set
            {
                this._update_time = value;
                SetAttributeValue("update_time", value);
            }
        }

        /// <summary>
        /// 作者
        /// </summary>
        private string _author;
        [DataMember]
        public string author
        {
            get
            {
                return this._author;
            }
            set
            {
                this._author = value;
                SetAttributeValue("author", value);
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdBy;
        [DataMember]
        public string createdBy
        {
            get
            {
                return this._createdBy;
            }
            set
            {
                this._createdBy = value;
                SetAttributeValue("createdBy", value);
            }
        }


        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdByName;
        [DataMember]
        public string createdByName
        {
            get
            {
                return this._createdByName;
            }
            set
            {
                this._createdByName = value;
                SetAttributeValue("createdByName", value);
            }
        }


        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime? _createdOn;
        [DataMember]
        public DateTime? createdOn
        {
            get
            {
                return this._createdOn;
            }
            set
            {
                this._createdOn = value;
                SetAttributeValue("createdOn", value);
            }
        }


        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedBy;
        [DataMember]
        public string modifiedBy
        {
            get
            {
                return this._modifiedBy;
            }
            set
            {
                this._modifiedBy = value;
                SetAttributeValue("modifiedBy", value);
            }
        }


        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedByName;
        [DataMember]
        public string modifiedByName
        {
            get
            {
                return this._modifiedByName;
            }
            set
            {
                this._modifiedByName = value;
                SetAttributeValue("modifiedByName", value);
            }
        }


        /// <summary>
        /// 修改日期
        /// </summary>
        private DateTime? _modifiedOn;
        [DataMember]
        public DateTime? modifiedOn
        {
            get
            {
                return this._modifiedOn;
            }
            set
            {
                this._modifiedOn = value;
                SetAttributeValue("modifiedOn", value);
            }
        }
    }
}

