using Blog.Core.Auth;
using Blog.Core.Module.Github.Model;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.Github
{
    public class GithubController : BaseApiController
    {
        [HttpGet, AllowAnonymous]
        public GithubConfig Config()
        {
            return new GithubService().GetConfig();
        }

        [HttpPost, AllowAnonymous]
        public LoginResponse OAuth(string code)
        {
            return new GithubService().LoginOrSignUp(code);
        }
    }
}
