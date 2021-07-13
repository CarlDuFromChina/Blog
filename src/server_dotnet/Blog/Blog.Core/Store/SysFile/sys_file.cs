using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Core.Store.SysFile
{
    [Entity("sys_file", "系统文件", true)]
    public partial class sys_file : BaseEntity
    {
        [DataMember]
        [Attr("sys_fileid", "实体id", AttrType.Varchar, 100)]
        public string sys_fileId
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
        /// 名称
        /// </summary>
        private string _name;
        [DataMember, Attr("name", "名称", AttrType.Varchar, 500)]
        public new string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                SetAttributeValue("name", value);
            }
        }

        /// <summary>
        /// 文件对象
        /// </summary>
        private string _objectid;
        [DataMember]
        [Attr("objectid", "文件对象", AttrType.Varchar, 100)]
        public string objectId
        {
            get
            {
                return this._objectid;
            }
            set
            {
                this._objectid = value;
                SetAttributeValue("objectid", value);
            }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        private string _file_path;
        [DataMember]
        [Attr("file_path", "文件路径", AttrType.Text)]
        public string file_path
        {
            get
            {
                return this._file_path;
            }
            set
            {
                this._file_path = value;
                SetAttributeValue("file_path", value);
            }
        }

        /// <summary>
        /// 哈希值
        /// </summary>
        private string _hash_code;
        [DataMember]
        [Attr("hash_code", "哈希值", AttrType.Varchar, 40)]
        public string hash_code
        {
            get
            {
                return this._hash_code;
            }
            set
            {
                this._hash_code = value;
                SetAttributeValue("hash_code", value);
            }
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        private string _file_type;
        [DataMember]
        [Attr("file_type", "文件类型", AttrType.Varchar, 100)]
        public string file_type
        {
            get
            {
                return this._file_type;
            }
            set
            {
                this._file_type = value;
                SetAttributeValue("file_type", value);
            }
        }

        /// <summary>
        /// 内容类型
        /// </summary>
        private string _content_type;
        [DataMember]
        [Attr("content_type", "内容类型", AttrType.Varchar, 100)]
        public string content_type
        {
            get
            {
                return this._content_type;
            }
            set
            {
                this._content_type = value;
                SetAttributeValue("content_type", value);
            }
        }
    }
}