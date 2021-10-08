
using Sixpence.EntityFramework.Entity;
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
        [Attr("wechat_materialid", "实体id", AttrType.Varchar, 100)]
        public string wechat_materialId
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
        /// 媒体id
        /// </summary>
        [DataMember]
        [Attr("media_id", "媒体id", AttrType.Varchar, 100)]
        public string media_id { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        [DataMember]
        [Attr("url", "地址", AttrType.Varchar, 400)]
        public string url { get; set; }

        /// <summary>
        /// 文件id
        /// </summary>
        [DataMember]
        [Attr("sys_fileid", "文件id", AttrType.Varchar, 100)]
        public string sys_fileid { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        [Attr("type", "类型", AttrType.Varchar, 100)]
        public string type { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [DataMember]
        [Attr("width", "宽度", AttrType.Int4)]
        public int? width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [DataMember]
        [Attr("height", "高度", AttrType.Int4)]
        public int? height { get; set; }

        /// <summary>
        /// 本地地址
        /// </summary>
        [DataMember]
        [Attr("local_url", "本地地址", AttrType.Varchar, 200)]
        public string local_url { get; set; }
    }
}

