using Sixpence.EntityFramework.Entity;
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
        [Attr("galleryid", "实体id", AttrType.Varchar, 100)]
        public string galleryId
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
        /// 标签
        /// </summary>
        [DataMember]
        [Attr("tags", "实体id", AttrType.Varchar, 100)]
        public string tags { get; set; }

        /// <summary>
        /// 预览图
        /// </summary>
        [DataMember]
        [Attr("preview_url", "预览图", AttrType.Varchar, 200)]
        public string preview_url { get; set; }

        /// <summary>
        /// 大图
        /// </summary>
        [DataMember]
        [Attr("image_url", "大图", AttrType.Varchar, 200)]
        public string image_url { get; set; }

        /// <summary>
        /// 预览图片id
        /// </summary>
        [DataMember]
        [Attr("previewid", "预览图片id", AttrType.Varchar, 100)]
        public string previewid { get; set; }

        /// <summary>
        /// 大图id
        /// </summary>
        [DataMember]
        [Attr("imageid", "大图id", AttrType.Varchar, 100)]
        public string imageid { get; set; }
    }
}

