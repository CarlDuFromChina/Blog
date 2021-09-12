using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Blog.Comments
{
    public partial class comments
    {
        [DataMember]
        public IEnumerable<comments> Comments { get; set; }
    }
}
