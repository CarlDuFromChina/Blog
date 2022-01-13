using Blog.Core.Auth.UserInfo;
using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Auth.Role;
using Blog.Core.Auth;


using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using Blog.Core.Extensions;

namespace Blog.Core.Module.Role
{
    public class SysRoleService : EntityService<sys_role>
    {
        #region 构造函数
        public SysRoleService() : base() { }

        public SysRoleService(IEntityManager manager) : base(manager) { }
        #endregion

        public IEnumerable<SelectOption> GetBasicRole()
        {
            var roles = Repository.GetAllEntity();
            var currentRoleId = Manager.QueryFirst<user_info>(UserIdentityUtil.GetCurrentUserId())?.roleid;
            var role = roles.FirstOrDefault(item => item.id == currentRoleId);

            return roles.Where(item => UserIdentityUtil.IsOwner(role.is_basic ? role.id : role.parent_roleid, item.is_basic ? item.id : item.parent_roleid))
                .Where(item => item.is_basic)
                .Select(item => new SelectOption(item.name, item.id));
        }


        public IEnumerable<SelectOption> GetRoles()
        {
            var roles = Repository.GetAllEntity();
            var currentRoleId = Manager.QueryFirst<user_info>(UserIdentityUtil.GetCurrentUserId())?.roleid;
            var role = roles.FirstOrDefault(item => item.id == currentRoleId);

            return roles.Where(item => UserIdentityUtil.IsOwner(role.is_basic ? role.id : role.parent_roleid, item.is_basic ? item.id : item.parent_roleid))
                .Select(item => new SelectOption(item.name, item.id));
        }

        public sys_role GetGuest() => Manager.QueryFirst<sys_role>("222222222-22222-2222-2222-222222222222");

        public bool AllowCreateOrUpdateRole(string roleid)
        {
            var currentRoleId = Manager.QueryFirst<user_info>(UserIdentityUtil.GetCurrentUserId())?.roleid;
            if (string.IsNullOrEmpty(currentRoleId))
            {
                return false;
            }
            var toRoleId = roleid;
            return Convert.ToInt32(toRoleId.FirstOrDefault().ToString()) >= Convert.ToInt32(currentRoleId.FirstOrDefault().ToString());
        }
    }
}
