using Blog.Core.Auth.Role.BasicRole;
using Blog.Core.Auth.UserInfo;
using Sixpence.EntityFramework.Entity;
using Blog.Core.Module.Role;
using Blog.Core.Module.SysEntity;
using Sixpence.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.EntityFramework.Broker;

namespace Blog.Core.Auth.Privilege
{
    public class SysRolePrivilegeService : EntityService<sys_role_privilege>
    {
        #region 构造函数
        public SysRolePrivilegeService()
        {
            _context = new EntityContext<sys_role_privilege>();
        }

        public SysRolePrivilegeService(IPersistBroker broker)
        {
            _context = new EntityContext<sys_role_privilege>(broker);
        }
        #endregion

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public IEnumerable<sys_role_privilege> GetUserPrivileges(string roleid, RoleType roleType)
        {
            var role = Broker.Retrieve<sys_role>(roleid);
            var privileges = ServiceContainer.ResolveAll<IRole>().FirstOrDefault(item => item.Role.GetDescription() == role.name).GetRolePrivilege();
            switch (roleType)
            {
                case RoleType.Entity:
                    return privileges.Where(item => item.object_type == nameof(Module.SysEntity.sys_entity));
                case RoleType.Menu:
                    return privileges.Where(item => item.object_type == nameof(Module.SysMenu.sys_menu));
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
            return Broker.RetrieveMultiple<sys_role_privilege>(sql, new Dictionary<string, object>() { { "@id", entityid } });
        }

        /// <summary>
        /// 批量更新或创建
        /// </summary>
        /// <param name="dataList"></param>
        public void BulkSave(List<sys_role_privilege> dataList)
        {
            Broker.BulkCreateOrUpdate(dataList);
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
                item.GetMissingPrivilege(Broker)
                    .Each(item =>
                    {
                        if (!item.Value.IsEmpty())
                        {
                            privileges.AddRange(item.Value);
                        }
                    });
            });

            Broker.ExecuteTransaction(() => Broker.BulkCreate(privileges));
        }
    }
}
