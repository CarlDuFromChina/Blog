using Sixpence.ORM.Entity;
using System;
using System.Runtime.Serialization;


namespace Blog.Core.Module.Gallery
{
    [Entity("gallery", "图库", true)]
    public partial class gallery : BaseEntity
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
        /// 标签
        /// </summary>
        [DataMember]
        [Column]
        public string tags { get; set; }

        /// <summary>
        /// 预览图
        /// </summary>
        [DataMember]
        [Column]
        public string preview_url { get; set; }

        /// <summary>
        /// 大图
        /// </summary>
        [DataMember]
        [Column]
        public string image_url { get; set; }

        /// <summary>
        /// 预览图片id
        /// </summary>
        [DataMember]
        [Column]
        public string previewid { get; set; }

        /// <summary>
        /// 大图id
        /// </summary>
        [DataMember]
        [Column]
        public string imageid { get; set; }
    }
}

