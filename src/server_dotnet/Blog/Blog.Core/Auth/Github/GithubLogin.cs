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
        public LoginResponse Login(object param)
        {
            var manager = EntityManagerFactory.GetManager();
            var githubService = new GithubAuthService(manager);
            var sysRoleService = new SysRoleService(manager);
            var logger = LogFactory.GetLogger("github");

            try
            {
                var code = param as string;
                var githubToken = githubService.GetAccessToken(code);
                var githubUser = githubService.GetUserInfo(githubToken);
                var user = manager.QueryFirst<user_info>("select * from user_info where github_id = @id", new Dictionary<string, object>() { { "@id", githubUser.id.ToString() } });

                if (user != null)
                {
                    return new LoginResponse()
                    {
                        result = true,
                        userName = code,
                        token = JwtHelper.CreateToken(new JwtTokenModel() { Code = user.code, Name = user.name, Role = user.code, Uid = user.id }),
                        userId = user.id,
                        message = "登录成功"
                    };
                }

                return manager.ExecuteTransaction(() =>
                {
                    var role = sysRoleService.GetGuest();
                    var id = Guid.NewGuid().ToString();
                    var avatarId = githubService.DownloadImage(githubUser.avatar_url, id);
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
                    manager.Create(user, false);
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
                    manager.Create(_authUser);

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
                logger.Error("Github 登录失败：" + ex.Message, ex);
                return new LoginResponse() { result = false, message = "Github 登录失败" };
            }
        }
    }
}
