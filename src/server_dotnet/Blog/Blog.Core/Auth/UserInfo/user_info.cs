using Blog.Core.Data;
using System;
using System.Runtime.Serialization;

namespace Blog.Core.Auth.UserInfo
{
    [Entity("user_info", "用户", true)]
    [KeyAttributes("请勿重复创建用户", "code")]
    [KeyAttributes("邮箱已被注册", "mailbox")]
    [KeyAttributes("手机号码已被注册", "cellphone")]
    public partial class user_info : BaseEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        [Attr("user_infoid", "用户id", AttrType.Varchar, 100)]
        public string user_infoId
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
                return _code;
            }
            set
            {
                _code = value;
                SetAttributeValue("code", value);
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        private int? _gender;
        [DataMember]
        [Attr("gender", "性别", AttrType.Int4)]
        public int? gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                SetAttributeValue("gender", value);
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        private string _genderName;
        [DataMember]
        [Attr("gendername", "性别", AttrType.Varchar, 100)]
        public string genderName
        {
            get
            {
                return _genderName;
            }
            set
            {
                _genderName = value;
                SetAttributeValue("genderName", value);
            }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        private string _realname;
        [DataMember]
        [Attr("realname", "真实姓名", AttrType.Varchar, 100)]
        public string realname
        {
            get
            {
                return _realname;
            }
            set
            {
                _realname = value;
                SetAttributeValue("realname", value);
            }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        private string _mailbox;
        [DataMember]
        [Attr("mailbox", "邮箱", AttrType.Varchar, 400)]
        public string mailbox
        {
            get
            {
                return _mailbox;
            }
            set
            {
                _mailbox = value;
                SetAttributeValue("mailbox", value);
            }
        }

        /// <summary>
        /// 个人介绍
        /// </summary>
        private string _introduction;
        [DataMember]
        [Attr("introduction", "个人介绍", AttrType.Text)]
        public string introduction
        {
            get
            {
                return _introduction;
            }
            set
            {
                _introduction = value;
                SetAttributeValue("introduction", value);
            }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        private string _cellphone;
        [DataMember]
        [Attr("cellphone", "手机号码", AttrType.Varchar, 100)]
        public string cellphone
        {
            get
            {
                return _cellphone;
            }
            set
            {
                _cellphone = value;
                SetAttributeValue("cellphone", value);
            }
        }

        /// <summary>
        /// 头像
        /// </summary>
        private string _avatar;
        [DataMember]
        [Attr("avatar", "头像", AttrType.Varchar, 40)]
        public string avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                _avatar = value;
                SetAttributeValue("avatar", value);
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

        private int? _stateCode;
        [DataMember]
        [Attr("statecode", "状态", AttrType.Int4)]
        public int? stateCode
        {
            get
            {
                return this._stateCode;
            }
            set
            {
                this._stateCode = value;
                SetAttributeValue("stateCode", value);
            }
        }

        private string _stateCodeName;
        [DataMember]
        [Attr("statecodename", "状态名", AttrType.Varchar, 100)]
        public string stateCodeName
        {
            get
            {
                return this._stateCodeName;
            }
            set
            {
                this._stateCodeName = value;
                SetAttributeValue("stateCodeName", value);
            }
        }
    }
}
