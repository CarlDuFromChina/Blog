using Sixpence.EntityFramework.Entity;
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
        [Attr("sys_paramid", "实体id", DataType.Varchar, 100)]
        public string sys_paramId
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

        [DataMember]
        [Attr("sys_paramgroupid", "选项集id", DataType.Varchar, 100)]
        public string sys_paramGroupId { get; set; }


        [DataMember]
        [Attr("sys_paramgroupidname", "选项集名", DataType.Varchar, 100)]
        public string sys_paramGroupIdName { get; set; }
    }
}