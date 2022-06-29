using Blog.Core.Auth.Privilege;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Module.SysEntity
{
    public class SysEntityPlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            if (context.Entity.GetEntityName() != "sys_entity") return;

            var manager = context.EntityManager;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    // 创建权限
                    new SysRolePrivilegeService(manager).CreateRoleMissingPrivilege();
                    // 重新注册权限并清除缓存
                    UserPrivilegesCache.Clear(manager);
                    break;
                case EntityAction.PostDelete:
                    var privileges = new SysRolePrivilegeService(manager).GetPrivileges(context.Entity.GetPrimaryColumn().Value).ToArray();
                    manager.Delete(privileges);
                    UserPrivilegesCache.Clear(manager);
                    break;
                default:
                    break;
            }
        }
    }
}
