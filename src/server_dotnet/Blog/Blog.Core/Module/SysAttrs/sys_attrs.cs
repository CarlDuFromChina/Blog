using Sixpence.ORM.Entity;
using System.Collections.Generic;
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
        [Attr("sys_attrsid", "实体字段id", DataType.Varchar, 100)]
        public string sys_attrsId
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
        [DataMember]
        [Attr("code", "编码", DataType.Varchar, 100)]
        public string code { get; set; }

        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("entityid", "实体id", DataType.Varchar, 100)]
        public string entityid { get; set; }

        /// <summary>
        /// 实体名
        /// </summary>
        [DataMember]
        [Attr("entityidname", "实体名", DataType.Varchar, 100)]
        public string entityidname { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        [DataMember]
        [Attr("attr_type", "字段类型", DataType.Varchar, 100)]
        public string attr_type { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        [DataMember]
        [Attr("attr_length", "字段长度", DataType.Int4)]
        public int? attr_length { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [DataMember]
        [Attr("isrequire", "是否必填", DataType.Int4)]
        public bool isrequire { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [DataMember]
        [Attr("default_value", "默认值", DataType.Varchar, 200)]
        public string default_value { get; set; }
    }
}