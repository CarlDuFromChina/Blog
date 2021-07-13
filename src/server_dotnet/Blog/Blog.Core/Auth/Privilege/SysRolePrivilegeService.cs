using Blog.Core.Auth.Role.BasicRole;
using Blog.Core.Data;
using Blog.Core.Module.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Auth.Privilege
{
    public class SysRolePrivilegeService : EntityService<sys_role_privilege>
    {
        #region 构造函数
        public SysRolePrivilegeService()
        {
            _cmd = new EntityCommand<sys_role_privilege>();
        }

        public SysRolePrivilegeService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<sys_role_privilege>(broker);
        }
        #endregion

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public IEnumerable<sys_role_privilege> GetUserPrivileges(string roleid)
        {
            var role = Broker.Retrieve<sys_role>(roleid);
            return ServiceContainer.ResolveAll<IBasicRole>().FirstOrDefault(item => item.Role.GetDescription() == role.name).GetRolePrivilege();

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
WHERE sys_entityid = @id";
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
    }
}
