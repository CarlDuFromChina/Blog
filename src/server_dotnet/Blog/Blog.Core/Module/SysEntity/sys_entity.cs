using Sixpence.ORM.Entity;
using Sixpence.ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Core.Module.SysEntity
{
    [Entity("sys_entity", "实体", true)]
    [KeyAttributes("实体不能重复创建", "code")]
    public partial class sys_entity : BaseEntity
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
        /// 编码
        /// </summary>
        [DataMember, Column]
        public string code { get; set; }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        [DataMember, Column]
        public bool? is_sys { get; set; }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        [DataMember, Column]
        public string is_sys_name { get; set; }
    }
}