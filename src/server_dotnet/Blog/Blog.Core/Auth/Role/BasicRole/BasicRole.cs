using Blog.Core.Auth.Privilege;
using Blog.Core.Data;
using Blog.Core.Module.Role;
using Blog.Core.Module.SysEntity;
using Blog.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Auth.Role.BasicRole
{
    public abstract class BasicRole
    {
        public IPersistBroker Broker;
        protected const string ROLE_PREFIX = "BasicRole";
        protected const string PRIVILEGE_PREFIX = "RolePrivilege";
        public string GetRoleKey => this.GetType().Name;

        public abstract SystemRole GetSystemRole();
        public SystemRole Role => GetSystemRole();
        public string RoleName => GetSystemRole().ToString();

        public BasicRole()
        {
            Broker = PersistBrokerFactory.GetPersistBroker();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public sys_role GetRole()
        {
            var key = $"{ROLE_PREFIX}_{RoleName}";
            return MemoryCacheUtil.GetOrAddCacheItem(key, () =>
            {
                var role = Broker.Retrieve<sys_role>("select * from sys_role where name = @name", new Dictionary<string, object>() { { "@name", Role.GetDescription() } });
                if (role == null)
                {
                    role = new sys_role()
                    {
                        Id = Guid.NewGuid().ToString(),
                        name = Role.GetDescription(),
                        is_basic = true
                    };
                    Broker.Create(role);
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
                var entityList = GetNoPrivilegeEntityList();
                if (!entityList.IsEmpty())
                {
                    CreateRolePrivilege();
                }
                var dataList = Broker.RetrieveMultiple<sys_role_privilege>("select * from sys_role_privilege where sys_roleidName = @name", new Dictionary<string, object>() { { "@name", Role.GetDescription() } });
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
        /// 创建角色权限
        /// </summary>
        /// <returns></returns>
        protected abstract void CreateRolePrivilege();

        /// <summary>
        /// 生成权限
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="role"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected sys_role_privilege GenerateRolePrivilege(sys_entity entity, sys_role role, int value)
        {
            var user = UserIdentityUtil.GetAdmin();
            var privilege = new sys_role_privilege()
            {
                Id = Guid.NewGuid().ToString(),
                sys_entityid = entity.Id,
                sys_entityidName = entity.name,
                sys_roleid = role.Id,
                sys_roleidName = role.name,
                createdBy = user.Id,
                createdByName = user.Name,
                createdOn = DateTime.Now,
                modifiedBy = user.Id,
                modifiedByName = user.Name,
                modifiedOn = DateTime.Now,
                privilege = value
            };
            return privilege;
        }

        /// <summary>
        /// 获取角色未生成实体权限的实体
        /// </summary>
        /// <param name="systemRole"></param>
        /// <returns></returns>
        protected IEnumerable<sys_entity> GetNoPrivilegeEntityList()
        {
            var sql = @"
select *
from sys_entity se 
where sys_entityid not in (
	select sys_entityid 
	from sys_role_privilege srp 
	where sys_roleid  = @roleid
)
";
            return Broker.RetrieveMultiple<sys_entity>(sql, new Dictionary<string, object>() { { "@roleid", GetRole()?.Id } });
        }
    }
}
