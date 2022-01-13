using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 是否执行成功
        /// </summary>
        [DataMember]
        [Column("is_success", "是否执行成功", DataType.Int4)]
        public bool is_success { get; set; }
    }
}
