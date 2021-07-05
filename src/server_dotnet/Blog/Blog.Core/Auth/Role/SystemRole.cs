using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Blog.Core.Auth.Role
{
    /// <summary>
    /// 基础系统角色
    /// </summary>
    public enum SystemRole
    {
        /// <summary>
        /// 拥有系统所有权限
        /// </summary>
        [Description("系统管理员")]
        Admin,
        /// <summary>
        /// 系统角色
        /// </summary>
        [Description("系统")]
        System,
        /// <summary>
        /// 拥有自己的权限
        /// </summary>
        [Description("用户")]
        User,
        /// <summary>
        /// 拥有只读权限
        /// </summary>
        [Description("访客")]
        Guest
    }
}
