using Blog.Core.Auth.Privilege;
using Sixpence.ORM.Entity;
using Blog.Core.Module.SysEntity;
using Blog.Core.Module.SysMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.ORM.Broker;

namespace Blog.Core.Auth.Role.BasicRole
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class Admin : BasicRole
    {
        public override Role Role => Role.Admin;

        public override IDictionary<string, IEnumerable<sys_role_privilege>> GetMissingPrivilege(IPersistBroker broker)
        {
            var dic = new Dictionary<string, IEnumerable<sys_role_privilege>>();

            dic.Add(RoleType.Entity.ToString(), GetMissingEntityPrivileges(broker).Select(item => GenerateRolePrivilege(item, this.GetSysRole(), (int)OperationType.Read + (int)OperationType.Write + (int)OperationType.Delete)));
            dic.Add(RoleType.Menu.ToString(), GetMissingMenuPrivileges(broker).Select(item => GenerateRolePrivilege(item, this.GetSysRole(), (int)OperationType.Read)));

            return dic;
        }
    }
}
