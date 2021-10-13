using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Blog.Core.Module.VersionScriptExecutionLog
{
    [Entity("version_script_execution_log", "版本脚本执行日志", true)]
    public class version_script_execution_log : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("version_script_execution_logid", "实体id", DataType.Varchar, 100)]
        public string version_script_execution_logid
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
        /// 是否执行成功
        /// </summary>
        [DataMember]
        [Attr("is_success", "是否执行成功", DataType.Int4)]
        public bool is_success { get; set; }
    }
}
