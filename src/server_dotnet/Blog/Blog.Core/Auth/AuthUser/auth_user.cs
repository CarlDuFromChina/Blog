using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Core.Auth
{
    [Entity("auth_user", "用户授权", true)]
    [KeyAttributes("请勿重复创建用户", "code")]
    public partial class auth_user : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [PrimaryColumn("id")]
        public string id { get; set; }

        [DataMember]
        [Column("code", "编码", DataType.Varchar, 100)]
        public string code { get; set; }

        [DataMember]
        [Column("password", "密码", DataType.Varchar, 100)]
        public string password { get; set; }

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
        [Column("roleidname", "角色权限名", DataType.Varchar, 100)]
        public string roleidName { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        [Column("user_infoid", "用户id", DataType.Varchar, 100)]
        public string user_infoid { get; set; }

        /// <summary>
        /// 锁定
        /// </summary>
        [DataMember]
        [Column("is_lock", "锁定", DataType.Int4)]
        public bool is_lock { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [DataMember]
        [Column("is_lockname", "是否锁定", DataType.Varchar, 100)]
        public string is_lockName { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        [DataMember]
        [Column("last_login_time", "上次登录时间", DataType.Timestamp)]
        public DateTime? last_login_time { get; set; }

        /// <summary>
        /// 尝试登录次数
        /// </summary>
        [DataMember]
        [Column("try_times", "尝试登录次数", DataType.Int4)]
        public int? try_times { get; set; }
    }
}