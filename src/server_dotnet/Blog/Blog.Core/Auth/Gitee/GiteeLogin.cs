using Blog.Core.Auth.UserInfo;
using Blog.Core.Module.Role;
using Sixpence.Common.Logging;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth.Gitee
{
    public class GiteeLogin : IThirdPartyLoginStrategy
    {
        public LoginResponse Login(object param)
        {
            var manager = EntityManagerFactory.GetManager();
            var giteeService = new GiteeAuthService(manager);
            var sysRoleService = new SysRoleService(manager);
            var logger = LoggerFactory.GetLogger("gitee");

            try
            {
                var code = param as string;
                var giteeToken = giteeService.GetAccessToken(code);
                var giteeUser = giteeService.GetGiteeUserInfo(giteeToken);
                var user = manager.QueryFirst<user_info>("select * from user_info where gitee_id = @id", new Dictionary<string, object>() { { "@id", giteeUser.id.ToString() } });
                
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
                    var avatarId = giteeService.DownloadImage(giteeUser.avatar_url, id);
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
                        statecode = true,
                        statecode_name = "启用"
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
                logger.Error("Gitee 登录失败：" + ex.Message, ex);
                return new LoginResponse() { result = false, message = "Gitee 登录失败" };
            }
        }
    }
}
