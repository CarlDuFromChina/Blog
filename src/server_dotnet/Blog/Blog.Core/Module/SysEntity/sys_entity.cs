using Sixpence.EntityFramework.Entity;
using Sixpence.EntityFramework.Models;
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
        [DataMember, Attr("sys_entityid", "实体id", AttrType.Varchar, 100, true)]
        public string sys_entityId
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
        /// 编码
        /// </summary>
        [DataMember, Attr("code", "编码", AttrType.Varchar, 100, true)]
        public string code { get; set; }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        [DataMember, Attr("is_sys", "是否系统实体", AttrType.Int4, true)]
        public bool is_sys { get; set; }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        [DataMember, Attr("is_sysname", "是否系统实体", AttrType.Varchar, 100, true)]
        public string is_sysName { get; set; }
    }
}