using Sixpence.ORM.Broker;
using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.SysConfig
{
    public class SysConfigService : EntityService<sys_config>
    {
        #region 构造函数
        public SysConfigService()
        {
            _context = new EntityContext<sys_config>();
        }

        public SysConfigService(IPersistBroker broker)
        {
            _context = new EntityContext<sys_config>(broker);
        }
        #endregion

        public object GetValue(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var sql = @"
select * from sys_config where code = @code;
";
                var data = Broker.Retrieve<sys_config>(sql, new Dictionary<string, object>() { { "@code", code } });
                return data?.value;
            }
            return "";
        }
    }
}