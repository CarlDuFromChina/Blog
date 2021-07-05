
using Blog.Core.Data;
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
        private string _wechat_materialid;
        [DataMember]
        [Attr("wechat_materialid", "实体id", AttrType.Varchar, 100)]
        public string wechat_materialId
        {
            get
            {
                return this._wechat_materialid;
            }
            set
            {
                this._wechat_materialid = value;
                SetAttributeValue("wechat_materialId", value);
            }
        }

        
        /// <summary>
        /// 媒体id
        /// </summary>
        private string _media_id;
        [DataMember]
        [Attr("media_id", "媒体id", AttrType.Varchar, 100)]
        public string media_id
        {
            get
            {
                return this._media_id;
            }
            set
            {
                this._media_id = value;
                SetAttributeValue("media_id", value);
            }
        }


        /// <summary>
        /// 地址
        /// </summary>
        private string _url;
        [DataMember]
        [Attr("url", "地址", AttrType.Varchar, 400)]
        public string url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
                SetAttributeValue("url", value);
            }
        }

        /// <summary>
        /// 文件id
        /// </summary>
        private string _sys_fileid;
        [DataMember]
        [Attr("sys_fileid", "文件id", AttrType.Varchar, 100)]
        public string sys_fileid
        {
            get
            {
                return this._sys_fileid;
            }
            set
            {
                this._sys_fileid = value;
                SetAttributeValue("sys_fileid", value);
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        private string _type;
        [DataMember]
        [Attr("type", "类型", AttrType.Varchar, 100)]
        public string type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                SetAttributeValue("type", value);
            }
        }

        /// <summary>
        /// 宽度
        /// </summary>
        private int? _width;
        [DataMember]
        [Attr("width", "宽度", AttrType.Int4)]
        public int? width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                SetAttributeValue("width", value);
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        private int? _height;
        [DataMember]
        [Attr("height", "高度", AttrType.Int4)]
        public int? height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                SetAttributeValue("height", value);
            }
        }

        /// <summary>
        /// 本地地址
        /// </summary>
        private string _local_url;
        [DataMember]
        [Attr("local_url", "本地地址", AttrType.Varchar, 200)]
        public string local_url
        {
            get
            {
                return _local_url;
            }
            set
            {
                _local_url = value;
                SetAttributeValue("local_url", value);
            }
        }

    }
}

