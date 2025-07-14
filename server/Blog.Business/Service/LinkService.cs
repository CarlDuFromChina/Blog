using Blog.Business.Entity;
using Sixpence.ORM.EntityManager;
using Sixpence.Web.Service;

namespace Blog.Business.Service
{
    public class LinkService : EntityService<Link>
    {
        #region 构造函数
        public LinkService() : base() { }
        public LinkService(IEntityManager manager) : base(manager) { }
        #endregion
    }
}
