using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Auth
{
    public class JwtTokenModel
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Role { get; set; }
    }
}
