
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
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        [Column]
        public string html_content { get; set; }

        /// <summary>
        /// 媒体id
        /// </summary>
        [DataMember]
        [Column]
        public string media_id { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [DataMember]
        [Column]
        public long? update_time { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [DataMember]
        [Column]
        public string author { get; set; }

        /// <summary>
        /// 封面图片id
        /// </summary>
        [DataMember]
        [Column]
        public string thumb_media_id { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Column]
        public string digest { get; set; }

        /// <summary>
        /// 原文地址
        /// </summary>
        [DataMember]
        [Column]
        public string content_source_url { get; set; }
    }
}

