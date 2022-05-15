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
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [DataMember]
        [Column("tags", "实体id", DataType.Varchar, 100)]
        public string tags { get; set; }

        /// <summary>
        /// 预览图
        /// </summary>
        [DataMember]
        [Column("preview_url", "预览图", DataType.Varchar, 200)]
        public string preview_url { get; set; }

        /// <summary>
        /// 大图
        /// </summary>
        [DataMember]
        [Column("image_url", "大图", DataType.Varchar, 200)]
        public string image_url { get; set; }

        /// <summary>
        /// 预览图片id
        /// </summary>
        [DataMember]
        [Column("previewid", "预览图片id", DataType.Varchar, 100)]
        public string previewid { get; set; }

        /// <summary>
        /// 大图id
        /// </summary>
        [DataMember]
        [Column("imageid", "大图id", DataType.Varchar, 100)]
        public string imageid { get; set; }
    }
}

