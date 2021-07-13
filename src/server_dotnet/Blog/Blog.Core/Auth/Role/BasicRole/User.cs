using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Auth.Role.BasicRole
{
    /// <summary>
    /// 普通用户
    /// </summary>
    public class User : BasicRole, IBasicRole
    {
        public override SystemRole GetSystemRole() => SystemRole.User;
        protected override void CreateRolePrivilege()
        {
            Broker.ExecuteTransaction(() =>
            {
                var entityList = GetNoPrivilegeEntityList();
                var dataList = entityList.Select(entity =>
                {
                    int privilege = (int)OperationType.Read + (int)OperationType.Write + (int)OperationType.Delete;
                    return GenerateRolePrivilege(entity, GetRole(), privilege);
                }).ToList();
                Broker.BulkCreate(dataList);
            });
        }
    }
}
