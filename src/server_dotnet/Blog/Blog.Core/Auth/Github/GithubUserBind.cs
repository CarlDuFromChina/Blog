using Blog.Core.Auth.UserInfo;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth.Github
{
    public class GithubUserBind : IThirdPartyBindStrategy
    {
        public void Bind(string code, string userid)
        {
            var manager = EntityManagerFactory.GetManager();
            var githubService = new GithubAuthService(manager);
            manager.ExecuteTransaction(() =>
            {
                var user = manager.QueryFirst<user_info>(userid);
                var githubToken = githubService.GetAccessToken(code);
                var githubUser = githubService.GetUserInfo(githubToken);
                user.github_id = githubUser.id.ToString();
                manager.Update(user);
            });
        }
    }
}
