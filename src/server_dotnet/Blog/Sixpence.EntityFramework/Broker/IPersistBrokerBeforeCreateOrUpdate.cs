using Sixpence.Common;
using Sixpence.Common.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Broker
{
    [ServiceRegister]
    public interface IPersistBrokerBeforeCreateOrUpdate
    {
        void Execute(PersistBrokerPluginContext context);
    }
}
