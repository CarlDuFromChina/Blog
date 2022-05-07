using Blog.Core.Auth;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Module.Role;
using Blog.Core.Module.System;
using log4net;
using Sixpence.Common.Logging;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth.Github
{
    public class GithubLogin : IThirdPartyLoginStrategy
    {
        IEntityManager Manager;
        GithubAuthService GithubService;
        SysRoleService SysRoleService;
        protected ILog Logger => LogFactory.GetLogger("github");

        public GithubLogin()
        {
            Manager = EntityManagerFactory.GetManager();
            GithubService = new GithubAuthService(Manager);
            SysRoleService = new SysRoleService(Manager);
        }

        public LoginResponse Login(object param)
        {
            try
            {
                var code = param as string;
                var githubToken = GithubService.GetAccessToken(code);
                var githubUser = GithubService.GetUserInfo(githubToken);
                var authUser = Manager.QueryFirst<auth_user>("select * from auth_user where code = @code", new Dictionary<string, object>() { { "@code", githubUser.id.ToString() } });

                if (authUser != null)
                {
                    return new LoginResponse()
                    {
                        result = true,
                        userName = code,
                        token = JwtHelper.CreateToken(new JwtTokenModel() { Code = authUser.code, Name = authUser.name, Role = authUser.code, Uid = authUser.id }),
                        userId = authUser.user_infoid,
                        message = "登录成功"
                    };
                }

                return Manager.ExecuteTransaction(() =>
                {
                    var role = SysRoleService.GetGuest();
                    var id = Guid.NewGuid().ToString();
                    var avatarId = GithubService.DownloadImage(githubUser.avatar_url, id);
                    var user = new user_info()
                    {
                        id = id,
                        code = githubUser.id.ToString(),
                        password = null,
                        name = githubUser.name,
                        mailbox = githubUser.email,
                        introduction = githubUser.bio,
                        avatar = avatarId,
                        roleid = role.id,
                        roleid_name = role.name,
                        github_id = githubUser.id.ToString(),
                        stateCode = 1,
                        stateCode_name = "启用"
                    };
                    Manager.Create(user, false);
                    var _authUser = new auth_user()
                    {
                        id = user.id,
                        name = user.name,
                        code = user.code,
                        roleid = user.roleid,
                        roleid_name = user.roleid_name,
                        user_infoid = user.id,
                        is_lock = false,
                        last_login_time = DateTime.Now,
                        password = null
                    };
                    Manager.Create(_authUser);

                    return new LoginResponse()
                    {
                        result = true,
                        userName = user.name,
                        token = JwtHelper.CreateToken(new JwtTokenModel() { Code = _authUser.code, Name = _authUser.name, Role = _authUser.code, Uid = _authUser.id }),
                        userId = _authUser.user_infoid,
                        message = "登录成功"
                    };
                });
            }
            catch (Exception ex)
            {
                Logger.Error("Github 登录失败：" + ex.Message, ex);
                return new LoginResponse() { result = false, message = "Github 登录失败" };
            }
        }
    }
}
