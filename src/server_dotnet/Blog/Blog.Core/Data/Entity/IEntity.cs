using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Data
{
    [ServiceRegister]
    public interface IEntity
    {
        string GetEntityName();
        string GetLogicalName();
        object GetAttributeValue(string attributeLogicalName);
        void SetAttributeValue(string attributeLogicalName, object value);
        bool IsSystemEntity();
        IEnumerable<Column> GetAttrs();
    }
}
