using Sixpence.ORM.Entity;
using Sixpence.ORM.Models;
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
        [PrimaryColumn()]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        [Column]
        public string code { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        [Column]
        public int? gender { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        [Column]
        public string gender_name { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [DataMember]
        [Column]
        public string realname { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        [Column]
        public string mailbox { get; set; }

        /// <summary>
        /// 个人介绍
        /// </summary>
        [DataMember]
        [Column]
        public string introduction { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DataMember]
        [Column]
        public string cellphone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [DataMember]
        [Column]
        public string avatar { get; set; }

        /// <summary>
        /// 生活照
        /// </summary>
        [DataMember]
        [Column]
        public string life_photo { get; set; }

        /// <summary>
        /// 角色权限id
        /// </summary>
        [DataMember]
        [Column]
        public string roleid { get; set; }

        /// <summary>
        /// Github ID
        /// </summary>
        [DataMember]
        [Column]
        public string github_id { get; set; }

        /// <summary>
        /// Gitee ID
        /// </summary>
        [DataMember]
        [Column]
        public string gitee_id { get; set; }

        /// <summary>
        /// 角色权限名
        /// </summary>
        [DataMember]
        [Column]
        public string roleid_name { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [DataMember]
        [Column]
        public bool? statecode { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [DataMember]
        [Column]
        public string statecode_name { get; set; }
    }
}
