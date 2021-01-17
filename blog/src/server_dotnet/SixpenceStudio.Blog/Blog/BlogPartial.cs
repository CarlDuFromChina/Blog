﻿using System;
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
        public string isSeriesName
        {
            get
            {
                return this.is_series == 0 ? "否" : "是";
            }
        }

        [DataMember]
        public string isShowName
        {
            get
            {
                return this.is_show == 0 ? "否" : "是";
            }
        }

        [DataMember]
        public int message { get; set; }
    }
}