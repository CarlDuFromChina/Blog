using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Core.Module.SysEntity
{
    [Entity("sys_entity", "实体", true)]
    [KeyAttributes("实体不能重复创建", "code")]
    public partial class sys_entity : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember, Attr("sys_entityid", "实体id", AttrType.Varchar, 100, true)]
        public string sys_entityId
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
        [DataMember, Attr("code", "编码", AttrType.Varchar, 100, true)]
        public string code
        {
            get
            {
                return _code;   
            }
            set
            {
                _code = value;
                SetAttributeValue("code", value);
            }
        }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        private bool _is_sys;
        [DataMember, Attr("is_sys", "是否系统实体", AttrType.Int4, true)]
        public bool is_sys
        {
            get
            {
                return _is_sys;
            }
            set
            {
                _is_sys = value;
                SetAttributeValue("is_sys", value);
            }
        }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        private string _is_sysName;
        [DataMember, Attr("is_sysname", "是否系统实体", AttrType.Varchar, 100, true)]
        public string is_sysName
        {
            get
            {
                return _is_sysName;
            }
            set
            {
                _is_sysName = value;
                SetAttributeValue("is_sysName", value);
            }
        }
    }
}