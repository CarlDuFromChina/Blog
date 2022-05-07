using Blog.Core.Auth.UserInfo;
using Blog.Core.Module.Role;
using Blog.Core.Module.System;
using log4net;
using Sixpence.Common.Logging;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth.Gitee
{
    public class GiteeLogin : IThirdPartyLoginStrategy
    {
        IEntityManager Manager;
        GiteeAuthService GiteeService;
        SysRoleService SysRoleService;
        ILog Logger => LogFactory.GetLogger("gitee");

        public GiteeLogin()
        {
            Manager = EntityManagerFactory.GetManager();
            GiteeService = new GiteeAuthService(Manager);
            SysRoleService = new SysRoleService(Manager);
        }

        public LoginResponse Login(object param)
        {
            try
            {
                var code = param as string;
                var giteeToken = GiteeService.GetAccessToken(code);
                var giteeUser = GiteeService.GetGiteeUserInfo(giteeToken);
                var authUser = Manager.QueryFirst<auth_user>("select * from auth_user where code = @code", new Dictionary<string, object>() { { "@code", giteeUser.id.ToString() } });
                
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
                    var avatarId = GiteeService.DownloadImage(giteeUser.avatar_url, id);
                    var user = new user_info()
                    {
                        id = id,
                        code = giteeUser.id.ToString(),
                        password = null,
                        name = giteeUser.name,
                        mailbox = giteeUser.email,
                        introduction = giteeUser.bio,
                        avatar = avatarId,
                        roleid = role.id,
                        roleid_name = role.name,
                        gitee_id = giteeUser.id.ToString(),
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
                Logger.Error("Gitee 登录失败：" + ex.Message, ex);
                return new LoginResponse() { result = false, message = "Gitee 登录失败" };
            }
        }
    }
}
