
using Newtonsoft.Json.Linq;
using Sixpence.ORM.Entity;
using System;
using System.Runtime.Serialization;


namespace Blog.WeChat.WeChatNews
{
    [Entity("wechat_news", "微信图文素材")]
    public partial class wechat_news : BaseEntity
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
        [Column("html_content", "内容", DataType.Text)]
        public string html_content { get; set; }

        /// <summary>
        /// 媒体id
        /// </summary>
        [DataMember]
        [Column("media_id", "媒体id", DataType.Varchar, 100)]
        public string media_id { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [DataMember]
        [Column("update_time", "上传时间", DataType.Timestamp)]
        public long? update_time { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [DataMember]
        [Column("author", "作者", DataType.Varchar, 100)]
        public string author { get; set; }

        /// <summary>
        /// 封面图片id
        /// </summary>
        [DataMember]
        [Column("thumb_media_id", "封面图片id", DataType.Varchar, 100)]
        public string thumb_media_id { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Column("digest", "摘要", DataType.Varchar, 500)]
        public string digest { get; set; }

        /// <summary>
        /// 原文地址
        /// </summary>
        [DataMember]
        [Column("content_source_url", "原文地址", DataType.Text)]
        public string content_source_url { get; set; }
    }
}

