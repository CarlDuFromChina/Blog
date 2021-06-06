using Newtonsoft.Json.Linq;
using SixpenceStudio.Core.Entity;
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
        /// 类型
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
        /// 类型
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
        private bool _is_series;
        [DataMember]
        public bool is_series
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
        /// 是否是系列
        /// </summary>
        private string _is_seriesName;
        [DataMember]
        public string is_seriesName
        {
            get
            {
                return this._is_seriesName;
            }
            set
            {
                this._is_seriesName = value;
                SetAttributeValue("is_seriesName", value);
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


        /// <summary>
        /// 禁止评论
        /// </summary>
        private bool _disable_comment;
        [DataMember]
        public bool disable_comment
        {
            get
            {
                return this._disable_comment;
            }
            set
            {
                this._disable_comment = value;
                SetAttributeValue("disable_comment", value);
            }
        }

        /// <summary>
        /// 封面
        /// </summary>
        private string _surfaceid;
        [DataMember]
        public string surfaceid
        {
            get
            {
                return this._surfaceid;
            }
            set
            {
                this._surfaceid = value;
                SetAttributeValue("surfaceid", value);
            }
        }


        /// <summary>
        /// 封面地址
        /// </summary>
        private string _surface_url;
        [DataMember]
        public string surface_url
        {
            get
            {
                return this._surface_url;
            }
            set
            {
                this._surface_url = value;
                SetAttributeValue("surface_url", value);
            }
        }


        /// <summary>
        /// 大封面
        /// </summary>
        private string _big_surfaceid;
        [DataMember]
        public string big_surfaceid
        {
            get
            {
                return this._big_surfaceid;
            }
            set
            {
                this._big_surfaceid = value;
                SetAttributeValue("big_surfaceid", value);
            }
        }


        /// <summary>
        /// 大封面地址
        /// </summary>
        private string _big_surface_url;
        [DataMember]
        public string big_surface_url
        {
            get
            {
                return this._big_surface_url;
            }
            set
            {
                this._big_surface_url = value;
                SetAttributeValue("big_surface_url", value);
            }
        }

        /// <summary>
        /// 是否展示
        /// </summary>
        private bool _is_show;
        [DataMember]
        public bool is_show
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
        /// 是否展示
        /// </summary>
        private string _is_showName;
        [DataMember]
        public string is_showName
        {
            get
            {
                return this._is_showName;
            }
            set
            {
                this._is_showName = value;
                SetAttributeValue("is_showName", value);
            }
        }
    }
}

