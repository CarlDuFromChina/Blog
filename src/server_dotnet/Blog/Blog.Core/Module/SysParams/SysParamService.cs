using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.SysParams
{
    public class SysParamService : EntityService<sys_param>
    {
        #region 构造函数
        public SysParamService() : base() { }

        public SysParamService(IEntityManager manger) : base(manger) { }
        #endregion
    }
}