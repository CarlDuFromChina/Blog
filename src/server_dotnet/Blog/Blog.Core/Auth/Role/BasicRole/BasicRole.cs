using Blog.Core.Auth.Privilege;
using Sixpence.ORM.Entity;
using Blog.Core.Module.Role;
using Blog.Core.Module.SysEntity;
using Blog.Core.Module.SysMenu;
using Sixpence.Common;
using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.ORM.EntityManager;

namespace Blog.Core.Auth.Role.BasicRole
{
    public abstract class BasicRole : IRole
    {
        public IEntityManager Manager = EntityManagerFactory.GetManager();
        protected const string ROLE_PREFIX = "BasicRole";
        protected const string PRIVILEGE_PREFIX = "RolePrivilege";

        public string GetRoleKey => this.GetType().Name;

        /// <summary>
        /// 系统角色
        /// </summary>
        public abstract Role Role { get; }

        /// <summary>
        /// 系统角色名
        /// </summary>
        public string RoleName => Role.ToString();

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public sys_role GetSysRole()
        {
            var key = $"{ROLE_PREFIX}_{RoleName}";
            return MemoryCacheUtil.GetOrAddCacheItem(key, () =>
            {
                var role = Manager.QueryFirst<sys_role>("select * from sys_role where name = @name", new Dictionary<string, object>() { { "@name", Role.GetDescription() } });
                if (role == null)
                {
                    role = new sys_role()
                    {
                        id = Guid.NewGuid().ToString(),
                        name = Role.GetDescription(),
                        is_basic = true
                    };
                    Manager.Create(role);
                }
                return role;
            }, DateTime.Now.AddHours(12));
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IEnumerable<sys_role_privilege> GetRolePrivilege()
        {
            var key = $"{PRIVILEGE_PREFIX}_{RoleName}";
            return MemoryCacheUtil.GetOrAddCacheItem(key, () =>
            {
                var sql = @"
SELECT * FROM sys_role_privilege
WHERE sys_roleidName = @name
";
                var dataList = Manager.Query<sys_role_privilege>(sql, new Dictionary<string, object>() { { "@name", Role.GetDescription() } });
                return dataList;
            }, DateTime.Now.AddHours(12));
        }

        /// <summary>
        /// 清除角色缓存
        /// </summary>
        public void ClearCache()
        {
            MemoryCacheUtil.RemoveCacheItem($"{PRIVILEGE_PREFIX}_{RoleName}");
            MemoryCacheUtil.RemoveCacheItem($"{ROLE_PREFIX}_{RoleName}");
        }

        /// <summary>
        /// 获取缺失权限
        /// </summary>
        /// <returns></returns>
        public abstract IDictionary<string, IEnumerable<sys_role_privilege>> GetMissingPrivilege(IEntityManager manager);

        /// <summary>
        /// 获取缺失实体权限
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<sys_entity> GetMissingEntityPrivileges(IEntityManager manager)
        {
            var role = GetSysRole();
            var paramList = new Dictionary<string, object>() { { "@id", role.id } };
            var sql = @"
SELECT * FROM sys_entity
WHERE id NOT IN (
	SELECT objectid FROM sys_role_privilege
	WHERE object_type = 'sys_entity' AND sys_roleid = @id
)
";
            return manager.Query<sys_entity>(sql, paramList);
        }

        /// <summary>
        /// 获取缺失菜单权限
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<sys_menu> GetMissingMenuPrivileges(IEntityManager manager)
        {
            var role = GetSysRole();
            var paramList = new Dictionary<string, object>() { { "@id", role.id } };
            var sql = @"
SELECT * FROM sys_menu
WHERE id NOT IN (
	SELECT objectid FROM sys_role_privilege
	WHERE object_type = 'sys_menu' AND sys_roleid = @id
)
";
            var dataList = manager.Query<sys_menu>(sql, paramList);
            return dataList;
        }

        /// <summary>
        /// 生成权限
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="role"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static sys_role_privilege GenerateRolePrivilege(BaseEntity entity, sys_role role, int value)
        {
            var user = UserIdentityUtil.GetSystem();
            var privilege = new sys_role_privilege()
            {
                id = Guid.NewGuid().ToString(),
                objectid = entity.PrimaryKey.Value,
                objectidName = entity.GetAttributeValue<string>("name"),
                object_type = entity.EntityName,
                sys_roleid = role.id,
                sys_roleidName = role.name,
                created_by = user.Id,
                created_by_name = user.Name,
                created_at = DateTime.Now,
                updated_by = user.Id,
                updated_by_name = user.Name,
                updated_at = DateTime.Now,
                privilege = value
            };
            return privilege;
        }
    }
}
