using Blog.Core.Auth;
using Blog.Core.Auth.Privilege;
using Blog.Core.Auth.Role.BasicRole;
using Sixpence.EntityFramework.Entity;
using Blog.Core.Module.Role;
using Sixpence.Common.Utils;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Sixpence.Common;
using Sixpence.EntityFramework.Broker;
using Sixpence.Common.IoC;

namespace Blog.Core.Module.SysRole
{
    public static class SysRoleExtension
    {
        public static IApplicationBuilder UseSysRole(this IApplicationBuilder app)
        {
            var roles = ServiceContainer.ResolveAll<IRole>();
            var broker = PersistBrokerFactory.GetPersistBroker();
            new SysRolePrivilegeService(broker).CreateRoleMissingPrivilege();

            // 权限读取到缓存
            roles.Each(item => MemoryCacheUtil.Set(item.GetRoleKey, new RolePrivilegeModel() { Role = item.GetSysRole(), Privileges = item.GetRolePrivilege() }, 3600 * 12));

            return app;
        }
    }
}
