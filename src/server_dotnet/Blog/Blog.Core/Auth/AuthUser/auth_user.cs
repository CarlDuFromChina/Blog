using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Core.Auth
{
    [Entity("auth_user", "用户授权", true)]
    [KeyAttributes("用户Id不能重复", "user_infoId")]
    public partial class auth_user : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("auth_userid", "实体id", AttrType.Varchar, 100)]
        public string auth_userId
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

        private string _code;
        [DataMember]
        [Attr("code", "编码", AttrType.Varchar, 100)]
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

        private string _password;
        [DataMember]
        [Attr("password", "密码", AttrType.Varchar, 100)]
        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                SetAttributeValue("password", value);
            }
        }

        /// <summary>
        /// 角色权限id
        /// </summary>
        private string _roleid;
        [DataMember]
        [Attr("roleid", "角色权限id", AttrType.Varchar, 100)]
        public string roleid
        {
            get
            {
                return _roleid;
            }
            set
            {
                _roleid = value;
                SetAttributeValue("roleid", value);
            }
        }

        /// <summary>
        /// 角色权限名
        /// </summary>
        private string _roleidName;
        [DataMember]
        [Attr("roleidname", "角色权限名", AttrType.Varchar, 100)]
        public string roleidName
        {
            get
            {
                return _roleidName;
            }
            set
            {
                _roleidName = value;
                SetAttributeValue("roleidName", value);
            }
        }

        /// <summary>
        /// 用户id
        /// </summary>
        private string _user_infoid;
        [DataMember]
        [Attr("user_infoid", "用户id", AttrType.Varchar, 100)]
        public string user_infoid
        {
            get
            {
                return _user_infoid;
            }
            set
            {
                _user_infoid = value;
                SetAttributeValue("user_infoid", value);
            }
        }

        /// <summary>
        /// 锁定
        /// </summary>
        private bool _is_lock;
        [DataMember]
        [Attr("is_lock", "锁定", AttrType.Int4)]
        public bool is_lock
        {
            get
            {
                return this._is_lock;
            }
            set
            {
                this._is_lock = value;
                SetAttributeValue("is_lock", value);
            }
        }

        /// <summary>
        /// 是否锁定
        /// </summary>
        private string _is_lockName;
        [DataMember]
        [Attr("is_lockname", "是否锁定", AttrType.Varchar, 100)]
        public string is_lockName
        {
            get
            {
                return this._is_lockName;
            }
            set
            {
                this._is_lockName = value;
                SetAttributeValue("is_lockName", value);
            }
        }
    }
}