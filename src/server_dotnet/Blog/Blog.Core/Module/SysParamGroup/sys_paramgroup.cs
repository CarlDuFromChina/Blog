using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Core.Module.SysParamGroup
{
    [Entity("sys_paramgroup", "选项集", true)]
    public partial class sys_paramgroup : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        [Attr("sys_paramgroupid", "实体id", DataType.Varchar, 100)]
        public string sys_paramGroupId
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
    }
}