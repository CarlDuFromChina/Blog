using Sixpence.ORM.Entity;
using Sixpence.ORM.Models;
using System;
using System.Runtime.Serialization;

namespace Blog.Core.Auth.UserInfo
{
    [Entity("user_info", "用户", true)]
    [KeyAttributes("昵称已被注册", "name")]
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
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        [Column("code", "编码", DataType.Varchar, 100)]
        public string code { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        [Column("gender", "性别", DataType.Int4)]
        public int? gender { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        [Column("gender_name", "性别", DataType.Varchar, 100)]
        public string gender_name { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [DataMember]
        [Column("realname", "真实姓名", DataType.Varchar, 100)]
        public string realname { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        [Column("mailbox", "邮箱", DataType.Varchar, 400)]
        public string mailbox { get; set; }

        /// <summary>
        /// 个人介绍
        /// </summary>
        [DataMember]
        [Column("introduction", "个人介绍", DataType.Text)]
        public string introduction { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DataMember]
        [Column("cellphone", "手机号码", DataType.Varchar, 100)]
        public string cellphone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [DataMember]
        [Column("avatar", "头像", DataType.Varchar, 40)]
        public string avatar { get; set; }

        /// <summary>
        /// 生活照
        /// </summary>
        [DataMember]
        [Column("life_photo", "生活照", DataType.Text)]
        public string life_photo { get; set; }

        /// <summary>
        /// 角色权限id
        /// </summary>
        [DataMember]
        [Column("roleid", "角色权限id", DataType.Varchar, 100)]
        public string roleid { get; set; }

        /// <summary>
        /// 角色权限名
        /// </summary>
        [DataMember]
        [Column("roleid_name", "角色权限名", DataType.Varchar, 100)]
        public string roleid_name { get; set; }

        [DataMember]
        [Column("statecode", "状态", DataType.Int4)]
        public int? stateCode { get; set; }

        [DataMember]
        [Column("stateCode_name", "状态名", DataType.Varchar, 100)]
        public string stateCode_name { get; set; }
    }
}
