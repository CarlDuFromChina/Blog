using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 文件对象
        /// </summary>
        [DataMember]
        [Column("objectid", "文件对象", DataType.Varchar, 100)]
        public string objectId { get; set; }

        /// <summary>
        /// 真实文件名
        /// </summary>
        [DataMember]
        [Column("real_name", "真实文件名", DataType.Varchar, 500)]
        public string real_name { get; set; }

        
        /// <summary>
        /// 文件路径
        /// </summary>
        [DataMember]
        [Column("file_path", "文件路径", DataType.Text)]
        public string file_path { get; set; }

        /// <summary>
        /// 哈希值
        /// </summary>
        [DataMember]
        [Column("hash_code", "哈希值", DataType.Varchar, 40)]
        public string hash_code { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [DataMember]
        [Column("file_type", "文件类型", DataType.Varchar, 100)]
        public string file_type { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        [DataMember]
        [Column("content_type", "内容类型", DataType.Varchar, 100)]
        public string content_type { get; set; }
    }
}