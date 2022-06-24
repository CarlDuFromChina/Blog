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
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        [Column]
        public string content { get; set; }


        /// <summary>
        /// 是否展示
        /// </summary>
        [DataMember]
        [Column(defaultValue: false)]
        public bool? is_show { get; set; }


        /// <summary>
        /// 书名
        /// </summary>
        [DataMember]
        [Column]
        public string book_title { get; set; }


        /// <summary>
        /// 封面
        /// </summary>
        [DataMember]
        [Column]
        public string surfaceid { get; set; }


        /// <summary>
        /// 封面链接
        /// </summary>
        [DataMember]
        [Column]
        public string surface_url { get; set; }


        /// <summary>
        /// 封面大图
        /// </summary>
        [DataMember]
        [Column]
        public string big_surfaceid { get; set; }


        /// <summary>
        /// 封面大图链接
        /// </summary>
        [DataMember]
        [Column]
        public string big_surface_url { get; set; }


        /// <summary>
        /// 禁止评论
        /// </summary>
        [DataMember]
        [Column]
        public bool? disable_comment { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Column]
        public string brief { get; set; }
    }
}

