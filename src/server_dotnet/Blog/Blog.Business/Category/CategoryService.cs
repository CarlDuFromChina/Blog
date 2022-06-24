using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Category
{
    public class CategoryService : EntityService<category>
    {
        #region 构造函数
        public CategoryService() : base() { }

        public CategoryService(IEntityManager manager) : base(manager) { }
        #endregion
    }
}
