using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth
{
    public class LoginRequest
    {
        public string code { get; set; }
        public string password { get; set; }
        public string publicKey { get; set; }
    }
}
