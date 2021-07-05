using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Auth.UserInfo
{
    public partial class user_info
    {
        [DataMember]
        public  string is_lockName { get; set; }
    }
}
