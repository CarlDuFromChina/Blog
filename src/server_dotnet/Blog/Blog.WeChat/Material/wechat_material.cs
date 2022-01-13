
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
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 媒体id
        /// </summary>
        [DataMember]
        [Column("media_id", "媒体id", DataType.Varchar, 100)]
        public string media_id { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        [DataMember]
        [Column("url", "地址", DataType.Varchar, 400)]
        public string url { get; set; }

        /// <summary>
        /// 文件id
        /// </summary>
        [DataMember]
        [Column("sys_fileid", "文件id", DataType.Varchar, 100)]
        public string sys_fileid { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        [Column("type", "类型", DataType.Varchar, 100)]
        public string type { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [DataMember]
        [Column("width", "宽度", DataType.Int4)]
        public int? width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [DataMember]
        [Column("height", "高度", DataType.Int4)]
        public int? height { get; set; }

        /// <summary>
        /// 本地地址
        /// </summary>
        [DataMember]
        [Column("local_url", "本地地址", DataType.Varchar, 200)]
        public string local_url { get; set; }
    }
}

