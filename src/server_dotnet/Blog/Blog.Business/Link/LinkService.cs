
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Link
{
    public class LinkService : EntityService<link>
    {
        #region 构造函数
        public LinkService() : base() { }
        public LinkService(IEntityManager manager) : base(manager) { }
        #endregion
    }
}
