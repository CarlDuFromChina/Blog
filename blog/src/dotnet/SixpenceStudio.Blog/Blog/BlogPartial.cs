using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Blog
{
    public partial class blog
    {
        [DataMember]
        public string imageId { get; set; }

        [DataMember]
        public string imageSrc { get; set; }

        [DataMember]
        public string isSeriesName
        {
            get
            {
                return this.is_series == 0 ? "否" : "是";
            }
        }
    }
}
