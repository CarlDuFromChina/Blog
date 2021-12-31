using Blog.Core.Auth.UserInfo;
using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Auth.Role;
using Blog.Core.Auth;
using Sixpence.EntityFramework.SelectOption;
using Sixpence.EntityFramework.Broker;
using Sixpence.Common.Utils;

namespace Blog.Core.Module.Role
{
    public class SysRoleService : EntityService<sys_role>
    {
        #region 构造函数
        public SysRoleService()
        {
            _context = new EntityContext<sys_role>();
        }

        public SysRoleService(IPersistBroker broker)
        {
            _context = new EntityContext<sys_role>(broker);
        }
        #endregion

        public IEnumerable<SelectOption> GetBasicRole()
        {
            var roles = _context.GetAllEntity();
            var currentRoleId = Broker.Retrieve<user_info>(UserIdentityUtil.GetCurrentUserId())?.roleid;
            var role = roles.FirstOrDefault(item => item.sys_roleId == currentRoleId);

            return roles.Where(item => UserIdentityUtil.IsOwner(role.is_basic ? role.Id : role.parent_roleid, item.is_basic ? item.Id : item.parent_roleid))
                .Where(item => item.is_basic)
                .Select(item => new SelectOption(item.name, item.Id));
        }


        public IEnumerable<SelectOption> GetRoles()
        {
            var roles = _context.GetAllEntity();
            var currentRoleId = Broker.Retrieve<user_info>(UserIdentityUtil.GetCurrentUserId())?.roleid;
            var role = roles.FirstOrDefault(item => item.sys_roleId == currentRoleId);

            return roles.Where(item => UserIdentityUtil.IsOwner(role.is_basic ? role.Id : role.parent_roleid, item.is_basic ? item.Id : item.parent_roleid))
                .Select(item => new SelectOption(item.name, item.Id));
        }

        public sys_role GetGuest() => Broker.Retrieve<sys_role>("222222222-22222-2222-2222-222222222222");

        public bool AllowCreateOrUpdateRole(string roleid)
        {
            var currentRoleId = Broker.Retrieve<user_info>(UserIdentityUtil.GetCurrentUserId())?.roleid;
            if (string.IsNullOrEmpty(currentRoleId))
            {
                return false;
            }
            var toRoleId = roleid;
            return Convert.ToInt32(toRoleId.FirstOrDefault().ToString()) >= Convert.ToInt32(currentRoleId.FirstOrDefault().ToString());
        }
    }
}
