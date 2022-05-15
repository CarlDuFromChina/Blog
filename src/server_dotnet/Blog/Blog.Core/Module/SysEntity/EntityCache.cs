
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Module.SysEntity
{
    public static class EntityCache
    {
        private const string EntityCachePrefix = "entity";
        private static readonly ConcurrentDictionary<string, sys_entity> Entities = new ConcurrentDictionary<string, sys_entity>();

        public static sys_entity GetEntity(string entityName)
        {
            return Entities.GetOrAdd(EntityCachePrefix + entityName, (key) =>
            {
                var manager = EntityManagerFactory.GetManager();
                var data = manager.QueryFirst<sys_entity>("select * from sys_entity where code = @name", new Dictionary<string, object>() { { "@name", entityName } });
                return data;
            });
        }

        public static IEnumerable<sys_entity> GetEntityList() => Entities.Values;
    }
}
