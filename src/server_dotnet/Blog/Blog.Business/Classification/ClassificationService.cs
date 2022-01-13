using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Classification
{
    public class ClassificationService : EntityService<classification>
    {
        #region 构造函数
        public ClassificationService() : base() { }

        public ClassificationService(IEntityManager manager) : base(manager) { }
        #endregion
    }
}
