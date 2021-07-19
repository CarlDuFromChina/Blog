using Blog.Core.Auth.Privilege;
using Blog.Core.Data;
using Blog.Core.Module.SysEntity;
using Blog.Core.Module.SysMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Auth.Role.BasicRole
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class Admin : BasicRole
    {
        public override Role Role => Role.Admin;

        public override IDictionary<string, IEnumerable<sys_role_privilege>> GetDefaultPrivilege()
        {
            var dic = new Dictionary<string, IEnumerable<sys_role_privilege>>();
            var entityContext = new EntityContext<sys_entity>(Broker);
            var menuContext = new EntityContext<sys_menu>(Broker);

            dic.Add(RoleType.Entity.ToString(), entityContext.GetAllEntity(false).Select(item => GenerateRolePrivilege(item, this.GetSysRole(), (int)OperationType.Read + (int)OperationType.Write + (int)OperationType.Delete)));
            dic.Add(RoleType.Menu.ToString(), menuContext.GetAllEntity(false).Select(item => GenerateRolePrivilege(item, this.GetSysRole(), (int)OperationType.Read)));

            return dic;
        }
    }
}
