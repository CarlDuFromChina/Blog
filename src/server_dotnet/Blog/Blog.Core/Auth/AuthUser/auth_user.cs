using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Blog.Core.Auth
{
    [Entity("auth_user", "用户授权", true)]
    [KeyAttributes("请勿重复创建用户", "code")]
    public partial class auth_user : BaseEntity
    {
        [PrimaryColumn, DataMember]
        public string id { get; set; }

        [Column, DataMember, Description("名称")]
        public string name { get; set; }

        [Column, DataMember, Description("编码")]
        public string code { get; set; }

        [Column, DataMember, Description("密码")]
        public string password { get; set; }

        [Column, DataMember, Description("角色权限id")]
        public string roleid { get; set; }

        [Column, DataMember, Description("角色权限名")]
        public string roleid_name { get; set; }

        [Column, DataMember, Description("用户id")]
        public string user_infoid { get; set; }

        [Column, DataMember, Description("锁定")]
        public bool? is_lock { get; set; }

        [Column, DataMember, Description("锁定")]
        public string is_lock_name { get; set; }

        [Column, DataMember, Description("上次登录时间")]
        public DateTime? last_login_time { get; set; }

        [Column, DataMember, Description("尝试登录次数")]
        public int? try_times { get; set; }
    }
}