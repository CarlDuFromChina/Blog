using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Core.Module.SysConfig
{
    [Entity("sys_config", "系统配置", true)]
    public partial class sys_config : BaseEntity
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
        [DataMember]
        [Column]
        public string code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        [Column]
        public string description { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [DataMember]
        [Column]
        public string value { get; set; }
    }
}