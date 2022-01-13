using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Analysis
{
    public class TimelineModel
    {
        public DateTime? created_at { get; set; }
        public string title { get; set; }
        public string created_by_name { get; set; }
    }
}
