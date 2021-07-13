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
        private string _code;
        [DataMember]
        [Attr("code", "编码", AttrType.Varchar, 100)]
        public string code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
                SetAttributeValue("code", value);
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string _description;
        [DataMember]
        [Attr("description", "描述", AttrType.Varchar, 100)]
        public string description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
                SetAttributeValue("description", value);
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        private string _value;
        [DataMember]
        [Attr("value", "描述", AttrType.Text)]
        public string value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                SetAttributeValue("value", value);
            }
        }
    }
}