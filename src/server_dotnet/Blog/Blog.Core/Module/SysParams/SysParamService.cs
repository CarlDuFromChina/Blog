using Sixpence.ORM.Broker;
using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.SysParams
{
    public class SysParamService : EntityService<sys_param>
    {
        #region 构造函数
        public SysParamService()
        {
            this._context = new EntityContext<sys_param>();
        }

        public SysParamService(IPersistBroker broker)
        {
            this._context = new EntityContext<sys_param>(broker);
        }
        #endregion
    }
}