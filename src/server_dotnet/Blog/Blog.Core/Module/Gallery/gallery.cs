using Blog.Core.Data;
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
        private string _tags;
        [DataMember]
        [Attr("tags", "实体id", AttrType.Varchar, 100)]
        public string tags
        {
            get
            {
                return this._tags;
            }
            set
            {
                this._tags = value;
                SetAttributeValue("tags", value);
            }
        }


        /// <summary>
        /// 预览图
        /// </summary>
        private string _preview_url;
        [DataMember]
        [Attr("preview_url", "预览图", AttrType.Varchar, 200)]
        public string preview_url
        {
            get
            {
                return this._preview_url;
            }
            set
            {
                this._preview_url = value;
                SetAttributeValue("preview_url", value);
            }
        }


        /// <summary>
        /// 大图
        /// </summary>
        private string _image_url;
        [DataMember]
        [Attr("image_url", "大图", AttrType.Varchar, 200)]
        public string image_url
        {
            get
            {
                return this._image_url;
            }
            set
            {
                this._image_url = value;
                SetAttributeValue("image_url", value);
            }
        }

        /// <summary>
        /// 预览图片id
        /// </summary>
        private string _previewid;
        [DataMember]
        [Attr("previewid", "预览图片id", AttrType.Varchar, 100)]
        public string previewid
        {
            get
            {
                return this._previewid;
            }
            set
            {
                this._previewid = value;
                SetAttributeValue("previewid", value);
            }
        }


        /// <summary>
        /// 大图id
        /// </summary>
        private string _imageid;
        [DataMember]
        [Attr("imageid", "大图id", AttrType.Varchar, 100)]
        public string imageid
        {
            get
            {
                return this._imageid;
            }
            set
            {
                this._imageid = value;
                SetAttributeValue("imageid", value);
            }
        }
    }
}

