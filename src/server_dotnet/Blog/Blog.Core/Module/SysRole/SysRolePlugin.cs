using Blog.Core.Auth.Privilege;
using Blog.Core.Auth.Role.BasicRole;
using Sixpence.ORM.Entity;
using Sixpence.Common;
using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.ORM.Broker;

namespace Blog.Core.Module.Role
{
    public class SysRolePlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            if (context.EntityName != "sys_role") return;

            var obj = context.Entity as sys_role;

            switch (context.Action)
            {
                case EntityAction.PostDelete:
                    {
                        AssertUtil.CheckBoolean<SpException>(context.Entity.GetAttributeValue("is_basic") is  true, "禁止删除基础角色", "D283AEBF-60CA-4DFF-B08D-6D3DD10AFBBA");
                    }
                    break;
                case EntityAction.PostCreate:
                    {
                        // 重新创建权限
                        var privileges = new SysRolePrivilegeService(context.Broker).GetUserPrivileges(obj.parent_roleid, RoleType.All).ToList();
                        privileges.Each(item =>
                        {
                            item.Id = Guid.NewGuid().ToString();
                            item.sys_roleid = obj.Id;
                            item.sys_roleidName = obj.name;
                            item.createdOn = new DateTime();
                            item.modifiedOn = new DateTime();
                        });
                        context.Broker.BulkCreate(privileges);
                        // 权限缓存清空
                        UserPrivilegesCache.Clear(context.Broker);
                    }
                    break;
                case EntityAction.PostUpdate:
                    {
                        // 删除所有权限
                        var privileges = new SysRolePrivilegeService(context.Broker).GetUserPrivileges(obj.Id, RoleType.All).ToList();
                        privileges.Each(item => context.Broker.Delete(item));
                        // 重新创建权限
                        privileges = new SysRolePrivilegeService(context.Broker).GetUserPrivileges(obj.parent_roleid, RoleType.All).ToList();
                        privileges.Each(item =>
                        {
                            item.Id = Guid.NewGuid().ToString();
                            item.sys_roleid = obj.Id;
                            item.sys_roleidName = obj.name;
                            item.createdOn = new DateTime();
                            item.modifiedOn = new DateTime();
                        });
                        context.Broker.BulkCreate(privileges);
                        // 权限缓存清空
                        UserPrivilegesCache.Clear(context.Broker);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
