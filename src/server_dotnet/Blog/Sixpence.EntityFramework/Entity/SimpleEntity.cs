using Sixpence.Common;
using Sixpence.Common.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Entity
{
    [IgnoreServiceRegister]
    public class SimpleEntity : BaseEntity
    {
        public SimpleEntity() { }
        public SimpleEntity(string entityName) : base(entityName) { }
        public SimpleEntity(string entityName, string id)
        {
            this.EntityName = entityName;
            this.Id = id;
        }

        public Dictionary<string, object> Attributes { get; set; }

        public override object GetAttributeValue(string name)
        {
            if (!ContainKey(name))
                return null;

            return Attributes[name];
        }

        public override T GetAttributeValue<T>(string name)
        {
            if (!ContainKey(name))
                return null;

            return Attributes[name] as T;
        }
        public override IDictionary<string, object> GetAttributes() => Attributes;

        public override IEnumerable<string> GetKeys() => Attributes.Keys;

        public override IEnumerable<object> GetValues() => Attributes.Values;

        public override bool ContainKey(string name) => Attributes.ContainsKey(name);

        public override void SetAttributeValue(string name, object value)
        {
            if (ContainKey(name))
            {
                Attributes[name] = value;
            }
        }
    }
}
