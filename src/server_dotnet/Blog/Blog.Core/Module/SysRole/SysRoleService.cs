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
            _context = new EntityContext<sys_role>();
        }

        public SysRoleService(IPersistBroker broker)
        {
            _context = new EntityContext<sys_role>(broker);
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

        public sys_role GetGuest() => Broker.Retrieve<sys_role>("222222222-22222-2222-2222-222222222222");
    }
}
