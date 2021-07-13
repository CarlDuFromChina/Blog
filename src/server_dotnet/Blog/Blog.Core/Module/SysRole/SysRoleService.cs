using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Module.Role
{
    public class SysRoleService : EntityService<sys_role>
    {
        #region 构造函数
        public SysRoleService()
        {
            _cmd = new EntityCommand<sys_role>();
        }

        public SysRoleService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<sys_role>(broker);
        }
        #endregion

        public IEnumerable<SelectOption> GetBasicRole()
        {
            var sql = @"
select sys_roleid as Value, name as Name  from sys_role
where is_basic = 1
";
            return Broker.Query<SelectOption>(sql);
        }
    }
}
