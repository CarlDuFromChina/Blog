using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.Blog.ReadingNote
{
    [EntityName("reading_note")]
    public partial class reading_note : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _reading_noteid;
        [DataMember]
        public string reading_noteId
        {
            get
            {
                return this._reading_noteid;
            }
            set
            {
                this._reading_noteid = value;
                SetAttributeValue("reading_noteId", value);
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
        /// 是否展示
        /// </summary>
        private int? _is_show;
        [DataMember]
        public int? is_show
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
        /// 书名
        /// </summary>
        private string _book_title;
        [DataMember]
        public string book_title
        {
            get
            {
                return this._book_title;
            }
            set
            {
                this._book_title = value;
                SetAttributeValue("book_title", value);
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
        /// 封面链接
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
        /// 封面大图
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
        /// 封面大图链接
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
        /// 禁止评论
        /// </summary>
        private int? _disable_comment;
        [DataMember]
        public int? disable_comment
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
        /// 摘要
        /// </summary>
        private int? _brief;
        [DataMember]
        public int? brief
        {
            get
            {
                return this._brief;
            }
            set
            {
                this._brief = value;
                SetAttributeValue("brief", value);
            }
        }
    }
}

