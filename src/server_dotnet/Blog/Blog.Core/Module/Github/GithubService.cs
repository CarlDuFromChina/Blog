using Blog.Core.Auth;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Module.Github.Model;
using Blog.Core.Module.Role;
using Blog.Core.Module.SysConfig;
using Newtonsoft.Json;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.Github
{
    public class GithubService
    {
        private IEntityManager Manager;

        #region 构造函数
        public GithubService()
        {
            Manager = EntityManagerFactory.GetManager();
        }

        public GithubService(IEntityManager manager)
        {
            Manager = manager;
        }
        #endregion

        public GithubConfig GetConfig()
        {
            var clientId = MemoryCacheUtil.GetOrAddCacheItem("github_oauth_client_id", () => new SysConfigService().GetValue("github_oauto_client_id").ToString());
            var clientSecret = MemoryCacheUtil.GetOrAddCacheItem("github_oauth_client_secret", () => new SysConfigService(Manager).GetValue("ClientId").ToString());
            return new GithubConfig()
            {
                client_id = clientId,
                client_secret = clientSecret
            };
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="code">临时码，有效期十分钟</param>
        public GithubAccessToken GetAccessToken(string code)
        {
            var config = GetConfig();
            var clientId = config.client_id;
            var clientSecret = config.client_secret;

            var param = new
            {
                client_id = clientId,
                client_secret = clientSecret,
                code = code
            };

            var response = HttpUtil.Post("https://github.com/login/oauth/access_token", JsonConvert.SerializeObject(param));
            var data = JsonConvert.DeserializeObject<GithubAccessToken>(response);
            return data;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public GithubUserInfo GetUserInfo(GithubAccessToken model)
        {
            var headers = new Dictionary<string, string>
            {
                { "token_type",  model.token_type },
                { "scope", model.scope },
                { "access_token", model.access_token }
            };

            var response = HttpUtil.Get("https://api.github.com/user", headers);
            var data = JsonConvert.DeserializeObject<GithubUserInfo>(response);
            return data;
        }

        /// <summary>
        /// 登录或注册
        /// </summary>
        /// <param name="code">临时码，有效期十分钟</param>
        public LoginResponse LoginOrSignUp(string code)
        {
            var githubToken = GetAccessToken(code);
            var githubUser = GetUserInfo(githubToken);
            var authUser = Manager.QueryFirst<auth_user>("select * from auth_user where code = @code", new Dictionary<string, object>() { { "@code", githubUser.id } });
            
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
                var role = new SysRoleService(Manager).GetGuest();
                var user = new user_info()
                {
                    id = Guid.NewGuid().ToString(),
                    code = githubUser.id.ToString(),
                    password = null,
                    name = githubUser.name,
                    mailbox = githubUser.email,
                    roleid = role.id,
                    roleid_name = role.name,
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
    }
}
