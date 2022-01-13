using System;
using System.Collections.Generic;
using Blog.Core.Module.SysParamGroup;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;

namespace Blog.Business.Classification
{
    public class ClassificationEntityOptionProvider : IEntityOptionProvider
    {
        public IEnumerable<SelectOption> GetOptions()
        {
            var manager = EntityManagerFactory.GetManager();
            return manager.Query<SelectOption>($"select code AS Value, name AS Name from classification");
        }
    }
}
