using Blog.Core.Auth.Privilege;
using Blog.Core.Data;
using Blog.Core.Module.Role;
using Blog.Core.Module.SysEntity;
using Blog.Core.Module.SysMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Auth.Role.BasicRole
{
    [ServiceRegister]
    public interface IRole
    {
        /// <summary>
        /// 获取唯一键
        /// </summary>
        string GetRoleKey { get; }

        /// <summary>
        /// 角色名
        /// </summary>
        Role Role { get; }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        sys_role GetSysRole();

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        IEnumerable<sys_role_privilege> GetRolePrivilege();

        /// <summary>
        /// 清除缓存
        /// </summary>
        void ClearCache();

        /// <summary>
        /// 获取初始化权限
        /// </summary>
        /// <returns></returns>
        IDictionary<string, IEnumerable<sys_role_privilege>> GetDefaultPrivilege();
    }

    public enum RoleType
    {
        Entity,
        Menu
    }

    public static class RoleTypeExtension
    {
        public static bool IsEmpty(this RoleType roleType)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var sql = "select * from sys_role_privilege where object_type = @type";
            var dic = new Dictionary<string, object>();
            switch (roleType)
            {
                case RoleType.Entity:
                    dic.Add("@type", nameof(sys_entity));
                    break;
                case RoleType.Menu:
                    dic.Add("@type", nameof(sys_menu));
                    break;
                default:
                    break;
            }
            return broker.Query<sys_role_privilege>(sql, dic).IsEmpty();
        }
    }
}
