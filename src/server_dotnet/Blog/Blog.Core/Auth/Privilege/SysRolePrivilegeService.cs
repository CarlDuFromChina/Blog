using Blog.Core.Auth.Role.BasicRole;
using Blog.Core.Auth.UserInfo;
using Sixpence.ORM.Entity;
using Blog.Core.Module.Role;
using Blog.Core.Module.SysEntity;
using Sixpence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Module.SysMenu;
using Sixpence.Common.IoC;
using Sixpence.ORM.Repository;
using Sixpence.ORM.EntityManager;

namespace Blog.Core.Auth.Privilege
{
    public class SysRolePrivilegeService : EntityService<sys_role_privilege>
    {
        #region 构造函数
        public SysRolePrivilegeService()
        {
            Repository = new Repository<sys_role_privilege>();
        }

        public SysRolePrivilegeService(IEntityManager manger)
        {
            Repository = new Repository<sys_role_privilege>(manger);
        }
        #endregion

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public IEnumerable<sys_role_privilege> GetUserPrivileges(string roleid, RoleType roleType)
        {
            var role = Manager.QueryFirst<sys_role>(roleid);
            var privileges = new List<sys_role_privilege>();

            if (role.is_basic)
            {
                privileges = ServiceContainer.ResolveAll<IRole>().FirstOrDefault(item => item.Role.GetDescription() == role.name).GetRolePrivilege().ToList();
            }
            else
            {
                var sql = @"
SELECT * FROM sys_role_privilege
WHERE sys_roleid = @id
";
                privileges = Manager.Query<sys_role_privilege>(sql, new Dictionary<string, object>() { { "@id", roleid } }).ToList();
            }

            switch (roleType)
            {
                case RoleType.All:
                    return privileges;
                case RoleType.Entity:
                    return privileges.Where(item => item.object_type == nameof(sys_entity));
                case RoleType.Menu:
                    return privileges.Where(item => item.object_type == nameof(sys_menu));
                default:
                    return new List<sys_role_privilege>();
            }
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="entityid"></param>
        /// <returns></returns>
        public IEnumerable<sys_role_privilege> GetPrivileges(string entityid)
        {
            var sql = @"
SELECT * FROM sys_role_privilege
WHERE objectid = @id";
            return Manager.Query<sys_role_privilege>(sql, new Dictionary<string, object>() { { "@id", entityid } });
        }

        /// <summary>
        /// 批量更新或创建
        /// </summary>
        /// <param name="dataList"></param>
        public void BulkSave(List<sys_role_privilege> dataList)
        {
            Manager.BulkCreateOrUpdate(dataList);
        }

        /// <summary>
        /// 自动生成权限
        /// </summary>
        public void CreateRoleMissingPrivilege()
        {
            var roles = ServiceContainer.ResolveAll<IRole>();
            var privileges = new List<sys_role_privilege>();

            roles.Each(item =>
            {
                item.GetMissingPrivilege(Manager)
                    .Each(item =>
                    {
                        if (!item.Value.IsEmpty())
                        {
                            privileges.AddRange(item.Value);
                        }
                    });
            });

            Manager.ExecuteTransaction(() => Manager.BulkCreate(privileges));
        }
    }
}
