using Blog.Core.Data;
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
            this._cmd = new EntityCommand<sys_param>();
        }

        public SysParamService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<sys_param>(broker);
        }
        #endregion
    }
}