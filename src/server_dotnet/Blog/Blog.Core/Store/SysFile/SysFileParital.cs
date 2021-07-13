using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Store.SysFile
{
    public partial class sys_file
    {
        [DataMember]
        public string DownloadUrl { get; set; }
    }
}
