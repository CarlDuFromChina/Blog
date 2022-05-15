
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.idea
{
    public class IdeaSerivice : EntityService<idea>
    {
        #region 构造函数
        public IdeaSerivice() : base() { }

        public IdeaSerivice(IEntityManager manager) : base(manager) { }
        #endregion

    }
}
