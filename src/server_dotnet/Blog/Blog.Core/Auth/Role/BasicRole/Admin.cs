using Blog.Core.Data;
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
    public class Admin : BasicRole, IBasicRole
    {
        public override SystemRole GetSystemRole() => SystemRole.Admin;

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
