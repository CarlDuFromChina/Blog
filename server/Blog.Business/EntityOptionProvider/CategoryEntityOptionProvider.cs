using Sixpence.Web.Entity;
using Sixpence.ORM.EntityManager;
using System.Collections.Generic;
using Sixpence.Web.Model;
using Sixpence.Web.EntityOptionProvider;

namespace Blog.Business.EntityOptionProvider
{
    public class CategoryEntityOptionProvider : IEntityOptionProvider
    {
        public IEnumerable<SelectOption> GetOptions()
        {
            var manager = EntityManagerFactory.GetManager();
            return manager.Query<SelectOption>($"select code AS Value, name AS Name from category");
        }
    }
}
