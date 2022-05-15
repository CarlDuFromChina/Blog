using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.JobHisotry
{
    public class JobHistoryService : EntityService<job_history>
    {
        #region 构造函数
        public JobHistoryService() : base() { }

        public JobHistoryService(IEntityManager manager) : base(manager) { }
        #endregion
    }
}
