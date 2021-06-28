using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.Blog.ReadingNote
{
    [Entity("reading_note", "读书笔记", false)]
    public partial class reading_note : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _reading_noteid;
        [DataMember]
        [Attr("reading_noteid", "读书笔记id", AttrType.Varchar, 100)]
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
        [Attr("content", "内容", AttrType.Text)]
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
        [Attr("is_show", "是否展示", AttrType.Int4)]
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
        [Attr("book_title", "书名", AttrType.Varchar, 100)]
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
        [Attr("surfaceid", "封面", AttrType.Varchar, 100)]
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
        [Attr("surface_url", "封面链接", AttrType.Varchar, 200)]
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
        [Attr("big_surfaceid", "封面大图", AttrType.Varchar, 100)]
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
        [Attr("big_surface_url", "封面大图链接", AttrType.Varchar, 200)]
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
        [Attr("disable_comment", "禁止评论", AttrType.Int4)]
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
        [Attr("brief", "摘要", AttrType.Varchar, 100)]
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

