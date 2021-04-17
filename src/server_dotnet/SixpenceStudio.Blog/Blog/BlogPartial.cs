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
        public int message { get; set; }
    }

    public class BlogActivityModel
    {
        public string created_date { get; set; }
        public int? count { get; set; }
    }
}
