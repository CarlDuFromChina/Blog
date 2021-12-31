using Sixpence.Common;
using Sixpence.Common.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Broker
{
    /// <summary>
    /// 持久化插件
    /// </summary>
    [ServiceRegister]
    public interface IPersistBrokerPlugin
    {
        /// <summary>
        /// 执行
        /// </summary>
        void Execute(PersistBrokerPluginContext context);
    }
}
