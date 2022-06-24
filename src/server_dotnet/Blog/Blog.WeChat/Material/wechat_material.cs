
using Sixpence.ORM.Entity;
using System;
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
        [DataMember]
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 媒体id
        /// </summary>
        [DataMember]
        [Column]
        public string media_id { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        [DataMember]
        [Column]
        public string url { get; set; }

        /// <summary>
        /// 文件id
        /// </summary>
        [DataMember]
        [Column]
        public string sys_fileid { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        [Column]
        public string type { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [DataMember]
        [Column]
        public int? width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [DataMember]
        [Column]
        public int? height { get; set; }

        /// <summary>
        /// 本地地址
        /// </summary>
        [DataMember]
        [Column]
        public string local_url { get; set; }
    }
}

