using Blog.Core.Data;
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
        [Attr("sys_configid", "实体id", AttrType.Varchar, 100)]
        public string sys_configId
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
        [Attr("code", "编码", AttrType.Varchar, 100)]
        public string code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        [Attr("description", "描述", AttrType.Varchar, 100)]
        public string description { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [DataMember]
        [Attr("value", "描述", AttrType.Text)]
        public string value { get; set; }
    }
}