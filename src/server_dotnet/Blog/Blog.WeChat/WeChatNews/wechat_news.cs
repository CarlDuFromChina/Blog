
using Newtonsoft.Json.Linq;
using Sixpence.EntityFramework.Entity;
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
        [Attr("wechat_newsid", "实体id", AttrType.Varchar, 100)]
        public string wechat_newsId
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
        [Attr("content", "内容", AttrType.JToken)]
        public JToken content { get; set; }


        /// <summary>
        /// 媒体id
        /// </summary>
        [DataMember]
        [Attr("media_id", "媒体id", AttrType.Varchar, 100)]
        public string media_id { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [DataMember]
        [Attr("update_time", "上传时间", AttrType.Timestamp)]
        public long? update_time { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [DataMember]
        [Attr("author", "作者", AttrType.Varchar, 100)]
        public string author { get; set; }

        /// <summary>
        /// 封面图片id
        /// </summary>
        [DataMember]
        [Attr("thumb_media_id", "封面图片id", AttrType.Varchar, 100)]
        public string thumb_media_id { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Attr("digest", "摘要", AttrType.Varchar, 500)]
        public string digest { get; set; }
    }
}

