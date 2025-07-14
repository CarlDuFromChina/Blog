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
    public class CategoryService : EntityService<Category>
    {
        #region 构造函数
        public CategoryService() : base() { }

        public CategoryService(IEntityManager manager) : base(manager) { }
        #endregion
    }
}
