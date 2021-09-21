using System;
using System.Collections.Generic;
using Blog.Core.Module.SysEntity;
using Blog.Core.Module.SysParamGroup;
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.SelectOption;

namespace Blog.Business.Classification
{
    public class ClassificationEntityOptionProvider : IEntityOptionProvider
    {
        public IEnumerable<SelectOption> GetOptions()
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            return broker.Query<SelectOption>($"select code AS Value, name AS Name from classification");
        }
    }
}
