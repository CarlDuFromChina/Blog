using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Classification
{
    public partial class classification
    {
        [DataMember]
        public string is_freeName
        {
            get
            {
                return this.is_free == 1 ? "是" : "否";
            }
        }
    }
}
