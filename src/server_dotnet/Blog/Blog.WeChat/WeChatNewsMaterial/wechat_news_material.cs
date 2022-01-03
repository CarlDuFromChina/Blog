using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Blog.WeChat.WeChatNewsMaterial
{
    [Entity("wechat_news_material", "微信图文消息素材")]
    public class wechat_news_material : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("wechat_news_materialid", "实体id", DataType.Varchar, 100)]
        public string wechat_news_materialId
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
            }
        }

        /// <summary>
        /// 本地文件id
        /// </summary>
        [DataMember]
        [Attr("fileid", "本地文件id", DataType.Varchar, 100)]
        public string fileid { get; set; }

        /// <summary>
        /// 本地地址
        /// </summary>
        [DataMember]
        [Attr("local_url", "本地地址", DataType.Text)]
        public string local_url { get; set; }

        /// <summary>
        /// 图片url
        /// </summary>
        [DataMember]
        [Attr("media_url", "图片url", DataType.Text)]
        public string media_url { get; set; }
    }
}
