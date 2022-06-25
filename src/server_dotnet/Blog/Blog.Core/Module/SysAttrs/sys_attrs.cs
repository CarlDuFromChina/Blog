using Sixpence.ORM.Entity;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Core.Module.SysAttrs
{
    [KeyAttributes("该实体字段已存在", "entityid", "code")]
    [Entity("sys_attrs", "实体字段", true)]
    public partial class sys_attrs : BaseEntity
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
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DataMember, Column, Description("编码")]
        public string code { get; set; }

        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember, Column, Description("实体id")]
        public string entityid { get; set; }

        /// <summary>
        /// 实体名
        /// </summary>
        [DataMember, Column, Description("实体名")]
        public string entityid_name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        [DataMember, Column, Description("字段类型")]
        public string attr_type { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        [DataMember, Column, Description("字段长度")]
        public int? attr_length { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [DataMember, Column, Description("是否必填")]
        public bool? isrequire { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [DataMember, Column, Description("默认值")]
        public string default_value { get; set; }
    }
}