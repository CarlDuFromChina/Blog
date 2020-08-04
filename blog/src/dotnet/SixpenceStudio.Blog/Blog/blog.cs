using Newtonsoft.Json.Linq;
using SixpenceStudio.Platform.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.Blog.Blog
{
    [EntityName("blog")]
    public partial class blog : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _blogid;
        [DataMember]
        public string blogId
        {
            get
            {
                return this._blogid;
            }
            set
            {
                this._blogid = value;
                SetAttributeValue("blogId", value);
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        private string _title;
        [DataMember]
        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
                SetAttributeValue("title", value);
            }
        }

        /// <summary>
        /// 分类
        /// </summary>
        private string _blog_type;
        [DataMember]
        public string blog_type
        {
            get
            {
                return this._blog_type;
            }
            set
            {
                this._blog_type = value;
                SetAttributeValue("blog_type", value);
            }
        }

        /// <summary>
        /// 分类
        /// </summary>
        private string _blog_typeName;
        [DataMember]
        public string blog_typeName
        {
            get
            {
                return this._blog_typeName;
            }
            set
            {
                this._blog_typeName = value;
                SetAttributeValue("blog_typeName", value);
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        private string _content;
        [DataMember]
        public string content
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
                SetAttributeValue("CreatedBy", value);
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
                SetAttributeValue("CreatedByName", value);
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
                SetAttributeValue("CreatedOn", value);
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
                SetAttributeValue("ModifiedBy", value);
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
                SetAttributeValue("ModifiedByName", value);
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
                SetAttributeValue("ModifiedOn", value);
            }
        }

        /// <summary>
        /// 阅读次数
        /// </summary>
        private int? _reading_times;
        [DataMember]
        public int? reading_times
        {
            get
            {
                return this._reading_times;
            }
            set
            {
                this._reading_times = value;
                SetAttributeValue("reading_times", value);
            }
        }

        /// <summary>
        /// 点赞次数
        /// </summary>
        private int? _upvote_times;
        [DataMember]
        public int? upvote_times
        {
            get
            {
                return this._upvote_times;
            }
            set
            {
                this._upvote_times = value;
                SetAttributeValue("upvote_times", value);
            }
        }

        /// <summary>
        /// 是否是系列
        /// </summary>
        private int? _is_series;
        [DataMember]
        public int? is_series
        {
            get
            {
                return this._is_series;
            }
            set
            {
                this._is_series = value;
                SetAttributeValue("is_series", value);
            }
        }

        /// <summary>
        /// 标签
        /// </summary>
        private JToken _tags;
        [DataMember]
        public JToken tags
        {
            get
            {
                return this._tags;
            }
            set
            {
                this._tags = value;
                SetAttributeValue("tags", value);
            }
        }
    }
}
