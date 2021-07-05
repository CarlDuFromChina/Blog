using Blog.Core.Auth.Privilege;
using Blog.Core.Auth.Role.BasicRole;
using Blog.Core.Utils;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.SysRole
{
    public static class SysRoleExtension
    {
        public static IApplicationBuilder UseSysRole(this IApplicationBuilder app)
        {
            ServiceContainer
                .ResolveAll<IBasicRole>()
                .Each(item => MemoryCacheUtil.Set(item.GetRoleKey, new RolePrivilegeModel() { Role = item.GetRole(), Privileges = item.GetRolePrivilege() }, 3600 * 12));
            return app;
        }
    }
}
