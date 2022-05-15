using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        [Column("content", "内容", DataType.Text)]
        public string content { get; set; }


        /// <summary>
        /// 是否展示
        /// </summary>
        [DataMember]
        [Column("is_show", "是否展示", DataType.Int4)]
        public int? is_show { get; set; }


        /// <summary>
        /// 书名
        /// </summary>
        [DataMember]
        [Column("book_title", "书名", DataType.Varchar, 100)]
        public string book_title { get; set; }


        /// <summary>
        /// 封面
        /// </summary>
        [DataMember]
        [Column("surfaceid", "封面", DataType.Varchar, 100)]
        public string surfaceid { get; set; }


        /// <summary>
        /// 封面链接
        /// </summary>
        [DataMember]
        [Column("surface_url", "封面链接", DataType.Varchar, 200)]
        public string surface_url { get; set; }


        /// <summary>
        /// 封面大图
        /// </summary>
        [DataMember]
        [Column("big_surfaceid", "封面大图", DataType.Varchar, 100)]
        public string big_surfaceid { get; set; }


        /// <summary>
        /// 封面大图链接
        /// </summary>
        [DataMember]
        [Column("big_surface_url", "封面大图链接", DataType.Varchar, 200)]
        public string big_surface_url { get; set; }


        /// <summary>
        /// 禁止评论
        /// </summary>
        [DataMember]
        [Column("disable_comment", "禁止评论", DataType.Int4)]
        public int? disable_comment { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Column("brief", "摘要", DataType.Text)]
        public string brief { get; set; }
    }
}

