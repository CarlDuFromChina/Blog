using Sixpence.EntityFramework.Entity;
using Sixpence.EntityFramework.Models;
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
        [DataMember]
        [Attr("code", "编码", AttrType.Varchar, 100)]
        public string code { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        [Attr("gender", "性别", AttrType.Int4)]
        public int? gender { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        [Attr("gendername", "性别", AttrType.Varchar, 100)]
        public string genderName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [DataMember]
        [Attr("realname", "真实姓名", AttrType.Varchar, 100)]
        public string realname { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        [Attr("mailbox", "邮箱", AttrType.Varchar, 400)]
        public string mailbox { get; set; }

        /// <summary>
        /// 个人介绍
        /// </summary>
        [DataMember]
        [Attr("introduction", "个人介绍", AttrType.Text)]
        public string introduction { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DataMember]
        [Attr("cellphone", "手机号码", AttrType.Varchar, 100)]
        public string cellphone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [DataMember]
        [Attr("avatar", "头像", AttrType.Varchar, 40)]
        public string avatar { get; set; }

        /// <summary>
        /// 角色权限id
        /// </summary>
        [DataMember]
        [Attr("roleid", "角色权限id", AttrType.Varchar, 100)]
        public string roleid { get; set; }

        /// <summary>
        /// 角色权限名
        /// </summary>
        [DataMember]
        [Attr("roleidname", "角色权限名", AttrType.Varchar, 100)]
        public string roleidName { get; set; }

        [DataMember]
        [Attr("statecode", "状态", AttrType.Int4)]
        public int? stateCode { get; set; }

        [DataMember]
        [Attr("statecodename", "状态名", AttrType.Varchar, 100)]
        public string stateCodeName { get; set; }
    }
}
