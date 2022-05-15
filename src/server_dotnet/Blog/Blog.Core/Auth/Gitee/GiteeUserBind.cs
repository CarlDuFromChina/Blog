using Blog.Core.Auth.UserInfo;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth.Gitee
{
    public  class GiteeUserBind : IThirdPartyBindStrategy
    {
        public void Bind(string code, string userid)
        {
            var manager = EntityManagerFactory.GetManager();
            var giteeAuthService = new GiteeAuthService(manager);
            manager.ExecuteTransaction(() =>
            {
                var user = manager.QueryFirst<user_info>(userid);
                var githubToken = giteeAuthService.GetAccessToken(code, userid);
                var githubUser = giteeAuthService.GetGiteeUserInfo(githubToken);
                user.gitee_id = githubUser.id.ToString();
                manager.Update(user);
            });
        }
    }
}
