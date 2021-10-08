using Sixpence.EntityFramework.Entity;
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
        /// 文件对象
        /// </summary>
        [DataMember]
        [Attr("objectid", "文件对象", AttrType.Varchar, 100)]
        public string objectId { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [DataMember]
        [Attr("file_path", "文件路径", AttrType.Text)]
        public string file_path { get; set; }

        /// <summary>
        /// 哈希值
        /// </summary>
        [DataMember]
        [Attr("hash_code", "哈希值", AttrType.Varchar, 40)]
        public string hash_code { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [DataMember]
        [Attr("file_type", "文件类型", AttrType.Varchar, 100)]
        public string file_type { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        [DataMember]
        [Attr("content_type", "内容类型", AttrType.Varchar, 100)]
        public string content_type { get; set; }
    }
}