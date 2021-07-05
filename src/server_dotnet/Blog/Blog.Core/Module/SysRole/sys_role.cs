using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Blog.Core.Module.Role
{
    [Entity("sys_role", "角色", true)]
    public partial class sys_role : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("sys_roleid", "角色id", AttrType.Varchar, 100)]
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

        
        /// <summary>
        /// 描述
        /// </summary>
        private string _description;
        [DataMember]
        [Attr("description", "描述", AttrType.Varchar, 200)]
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
        /// 是否基础角色
        /// </summary>
        private bool _is_basic;
        [DataMember]
        [Attr("is_basic", "是否基础角色", AttrType.Int4)]
        public bool is_basic
        {
            get
            {
                return this._is_basic;
            }
            set
            {
                this._is_basic = value;
                SetAttributeValue("is_basic", value);
            }
        }

        /// <summary>
        /// 是否基础角色
        /// </summary>
        private string _is_basicName;
        [DataMember]
        [Attr("is_basicname", "是否基础角色", AttrType.Varchar, 100)]
        public string is_basicName
        {
            get
            {
                return this._is_basicName;
            }
            set
            {
                this._is_basicName = value;
                SetAttributeValue("is_basicName", value);
            }
        }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        private bool _is_sys;
        [DataMember]
        [Attr("is_sys", "是否系统实体", AttrType.Int4)]
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
        [DataMember]
        [Attr("is_sysname", "是否系统实体", AttrType.Varchar, 100)]
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

        /// <summary>
        /// 继承角色
        /// </summary>
        private string _parent_roleid;
        [DataMember]
        [Attr("parent_roleid", "继承角色", AttrType.Varchar, 100)]
        public string parent_roleid
        {
            get
            {
                return this._parent_roleid;
            }
            set
            {
                this._parent_roleid = value;
                SetAttributeValue("parent_roleid", value);
            }
        }


        /// <summary>
        /// 继承角色
        /// </summary>
        private string _parent_roleidName;
        [DataMember]
        [Attr("parent_roleidname", "继承角色", AttrType.Varchar, 100)]
        public string parent_roleidName
        {
            get
            {
                return this._parent_roleidName;
            }
            set
            {
                this._parent_roleidName = value;
                SetAttributeValue("parent_roleidName", value);
            }
        }
    }
}

