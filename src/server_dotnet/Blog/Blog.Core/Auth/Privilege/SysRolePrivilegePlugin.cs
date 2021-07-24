using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth.Privilege
{
    public class SysRolePrivilegePlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            if (context.EntityName != nameof(sys_role_privilege)) return;

            switch (context.Action)
            {
                case EntityAction.PostCreate:
                case EntityAction.PostDelete:
                case EntityAction.PostUpdate:
                    UserPrivilegesCache.Clear(context.Broker);
                    break;
                default:
                    break;
            }
        }
    }
}
