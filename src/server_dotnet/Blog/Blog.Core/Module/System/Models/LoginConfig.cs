using Blog.Core.Auth.Gitee.Model;
using Blog.Core.Auth.Github.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.System.Models
{
    public class LoginConfig
    {
        public GithubConfig github { get; set; }
        public GiteeConfig gitee { get; set; }
    }
}
