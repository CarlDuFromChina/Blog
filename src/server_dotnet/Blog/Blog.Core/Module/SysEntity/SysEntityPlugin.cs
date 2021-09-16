using Blog.Core.Auth.Privilege;
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Module.SysEntity
{
    public class SysEntityPlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            if (context.EntityName != "sys_entity") return;

            var broker = context.Broker;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    // 重新注册权限并清除缓存
                    UserPrivilegesCache.Clear(broker);
                    break;
                case EntityAction.PostDelete:
                    var privileges = new SysRolePrivilegeService(broker).GetPrivileges(context.Entity.Id).ToArray();
                    broker.Delete(privileges);
                    UserPrivilegesCache.Clear(broker);
                    break;
                default:
                    break;
            }
        }
    }
}
