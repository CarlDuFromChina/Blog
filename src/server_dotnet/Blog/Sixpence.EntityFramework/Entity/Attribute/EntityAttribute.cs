using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Entity
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EntityAttribute : Attribute
    {
        public EntityAttribute(string name, string logicalName, bool isSystemEntity = false)
        {
            this.Name = name;
            this.LogicalName = logicalName;
            this.IsSystemEntity = isSystemEntity;
        }

        public string Name { get; set; }
        public string LogicalName { get; set; }
        public bool IsSystemEntity { get; set; }
    }
}
