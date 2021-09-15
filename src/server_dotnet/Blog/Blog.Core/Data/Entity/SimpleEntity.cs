using Sixpence.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Data
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
    }
}
