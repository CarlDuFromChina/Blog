using Blog.Core.Entity;
using Blog.Core.Module.SysParamGroup;
using Sixpence.ORM.EntityManager;
using System.Collections.Generic;

namespace Blog.Business.Category
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
