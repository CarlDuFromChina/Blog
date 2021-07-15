using Blog.Core.Data;
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
        [Attr("version_script_execution_logid", "实体id", AttrType.Varchar, 100)]
        public string sys_roleId
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
        
        private bool _is_success;

        /// <summary>
        /// 是否执行成功
        /// </summary>
        [DataMember]
        [Attr("is_success", "是否执行成功", AttrType.Int4)]
        public bool is_success
        {
            get
            {
                return this._is_success;
            }
            set
            {
                this._is_success = value;
                SetAttributeValue("is_success", value);
            }
        }
    }
}
