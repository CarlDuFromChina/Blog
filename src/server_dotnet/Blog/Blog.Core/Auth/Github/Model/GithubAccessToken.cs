using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth.Github.Model
{
    public class GithubAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
    }
}
