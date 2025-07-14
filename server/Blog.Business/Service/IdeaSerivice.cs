using Blog.Business.Entity;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using Sixpence.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Service
{
    public class IdeaSerivice : EntityService<Idea>
    {
        #region 构造函数
        public IdeaSerivice() : base() { }

        public IdeaSerivice(IEntityManager manager) : base(manager) { }
        #endregion

    }
}
