using Blog.Core.Module.SysParamGroup;
using Sixpence.ORM.Broker;
using Sixpence.ORM.SelectOption;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.SysEntity
{
    public class SysEntityEntityOptionProvider : IEntityOptionProvider
    {
        public IEnumerable<SelectOption> GetOptions()
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            return broker.Query<SelectOption>($"select code AS Value, name AS Name from sys_entity");
        }
    }
}
