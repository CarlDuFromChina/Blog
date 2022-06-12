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
        /// 名称
        /// </summary>
        [DataMember]
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 文件对象
        /// </summary>
        [DataMember]
        [Column]
        public string objectId { get; set; }

        /// <summary>
        /// 真实文件名
        /// </summary>
        [DataMember]
        [Column]
        public string real_name { get; set; }

        
        /// <summary>
        /// 文件路径
        /// </summary>
        [DataMember]
        [Column]
        public string file_path { get; set; }

        /// <summary>
        /// 哈希值
        /// </summary>
        [DataMember]
        [Column]
        public string hash_code { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [DataMember]
        [Column]
        public string file_type { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        [DataMember]
        [Column]
        public string content_type { get; set; }
    }
}