using Blog.Core.Auth;
using Blog.Core.Auth.Privilege;
using Blog.Core.Auth.Role.BasicRole;
using Blog.Core.Data;
using Blog.Core.Module.Role;
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
            var roles = ServiceContainer.ResolveAll<IRole>();
            var broker = PersistBrokerFactory.GetPersistBroker();

            var privileges = new List<sys_role_privilege>();
            var isEntityEmpty = RoleType.Entity.IsEmpty();
            var isMenuEmpty = RoleType.Menu.IsEmpty();
            roles.Each(item =>
            {
                var defaultValue = item.GetDefaultPrivilege();

                if (isEntityEmpty)
                {
                    privileges.AddRange(defaultValue[RoleType.Entity.ToString()]);
                }

                if (isMenuEmpty)
                {
                    privileges.AddRange(defaultValue[RoleType.Menu.ToString()]);
                }
            });

            broker.ExecuteTransaction(() => broker.BulkCreate(privileges));

            // 权限读取到缓存
            roles.Each(item => MemoryCacheUtil.Set(item.GetRoleKey, new RolePrivilegeModel() { Role = item.GetSysRole(), Privileges = item.GetRolePrivilege() }, 3600 * 12));

            return app;
        }
    }
}
