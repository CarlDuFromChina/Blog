using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 本地文件id
        /// </summary>
        [DataMember, Column, Description("本地文件id")]
        public string fileid { get; set; }

        /// <summary>
        /// 本地地址
        /// </summary>
        [DataMember, Column, Description("本地地址")]
        public string local_url { get; set; }

        /// <summary>
        /// 图片url
        /// </summary>
        [DataMember, Column, Description("图片url")]
        public string media_url { get; set; }
    }
}
