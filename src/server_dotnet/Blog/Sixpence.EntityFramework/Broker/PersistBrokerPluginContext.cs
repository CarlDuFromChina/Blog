using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Broker
{
    /// <summary>
    /// 持久化插件上下文
    /// </summary>
    public class PersistBrokerPluginContext
    {
        public IPersistBroker Broker { get; set; }
        public BaseEntity Entity { get; set; }
        public string EntityName { get; set; }
        public EntityAction Action { get; set; }
    }

    /// <summary>
    /// 实体操作类型
    /// </summary>
    public enum EntityAction
    {
        [Description("创建前")]
        PreCreate,
        [Description("创建后")]
        PostCreate,
        [Description("更新前")]
        PreUpdate,
        [Description("更新后")]
        PostUpdate,
        [Description("删除前")]
        PreDelete,
        [Description("删除后")]
        PostDelete
    }
}
