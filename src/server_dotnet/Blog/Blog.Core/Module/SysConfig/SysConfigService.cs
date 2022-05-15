using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.SysConfig
{
    public class SysConfigService : EntityService<sys_config>
    {
        #region 构造函数
        public SysConfigService() : base() { }

        public SysConfigService(IEntityManager manager) : base(manager) { }
        #endregion

        public object GetValue(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var sql = @"
select * from sys_config where code = @code;
";
                var data = Manager.QueryFirst<sys_config>(sql, new Dictionary<string, object>() { { "@code", code } });
                return data?.value;
            }
            return "";
        }
    }
}