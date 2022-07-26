
using Sixpence.ORM.Entity;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Blog.WeChat.Material
{
    [Entity("wechat_material", "微信素材")]
    public partial class wechat_material : BaseEntity
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
        /// 媒体id
        /// </summary>
        [DataMember, Column, Description("媒体id")]
        public string media_id { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [DataMember, Column, Description("地址")]
        public string url { get; set; }

        /// <summary>
        /// 文件id
        /// </summary>
        [DataMember, Column, Description("文件id")]
        public string sys_fileid { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DataMember, Column, Description("类型")]
        public string type { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [DataMember, Column, Description("宽度")]
        public int? width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [DataMember, Column, Description("高度")]
        public int? height { get; set; }

        /// <summary>
        /// 本地地址
        /// </summary>
        [DataMember, Column, Description("本地地址")]
        public string local_url { get; set; }
    }
}

