using Blog.Core.Data;
using System;
using System.Runtime.Serialization;

namespace Blog.ReadingNote
{
    [Entity("reading_note", "读书笔记", false)]
    public partial class reading_note : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("reading_noteid", "读书笔记id", AttrType.Varchar, 100)]
        public string reading_noteId
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
        /// 内容
        /// </summary>
        [DataMember]
        [Attr("content", "内容", AttrType.Text)]
        public string content { get; set; }


        /// <summary>
        /// 是否展示
        /// </summary>
        [DataMember]
        [Attr("is_show", "是否展示", AttrType.Int4)]
        public int? is_show { get; set; }


        /// <summary>
        /// 书名
        /// </summary>
        [DataMember]
        [Attr("book_title", "书名", AttrType.Varchar, 100)]
        public string book_title { get; set; }


        /// <summary>
        /// 封面
        /// </summary>
        [DataMember]
        [Attr("surfaceid", "封面", AttrType.Varchar, 100)]
        public string surfaceid { get; set; }


        /// <summary>
        /// 封面链接
        /// </summary>
        [DataMember]
        [Attr("surface_url", "封面链接", AttrType.Varchar, 200)]
        public string surface_url { get; set; }


        /// <summary>
        /// 封面大图
        /// </summary>
        [DataMember]
        [Attr("big_surfaceid", "封面大图", AttrType.Varchar, 100)]
        public string big_surfaceid { get; set; }


        /// <summary>
        /// 封面大图链接
        /// </summary>
        [DataMember]
        [Attr("big_surface_url", "封面大图链接", AttrType.Varchar, 200)]
        public string big_surface_url { get; set; }


        /// <summary>
        /// 禁止评论
        /// </summary>
        [DataMember]
        [Attr("disable_comment", "禁止评论", AttrType.Int4)]
        public int? disable_comment { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Attr("brief", "摘要", AttrType.Varchar, 100)]
        public int? brief { get; set; }
    }
}

