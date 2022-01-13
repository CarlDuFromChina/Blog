using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Core.Module.SysParams
{
    [Entity("sys_param", "选项", true)]
    public partial class sys_param : BaseEntity
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
        /// 编码
        /// </summary>
        [DataMember]
        [Column("code", "编码", DataType.Varchar, 100)]
        public string code { get; set; }

        [DataMember]
        [Column("sys_paramgroupid", "选项集id", DataType.Varchar, 100)]
        public string sys_paramGroupId { get; set; }


        [DataMember]
        [Column("sys_paramgroupidname", "选项集名", DataType.Varchar, 100)]
        public string sys_paramGroupIdName { get; set; }
    }
}