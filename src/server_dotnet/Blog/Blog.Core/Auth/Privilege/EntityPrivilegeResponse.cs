using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth.Privilege
{
    public class EntityPrivilegeResponse
    {
        public bool create { get; set; }
        public bool read { get; set; }
        public bool delete { get; set; }
    }
}
