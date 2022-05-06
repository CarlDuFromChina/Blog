using Blog.Core.Auth;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Config;
using Blog.Core.Module.Github.Model;
using Blog.Core.Module.Role;
using Blog.Core.Module.SysConfig;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using log4net;
using Newtonsoft.Json;
using Sixpence.Common;
using Sixpence.Common.IoC;
using Sixpence.Common.Logging;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.Github
{
    public class GithubService
    {
        protected IEntityManager Manager;
        protected ILog Logger => LogFactory.GetLogger("github");

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
            var configService = new SysConfigService(Manager);
            var clientId = configService.GetValue("github_oauth_client_id")?.ToString();
            var clientSecret = configService.GetValue("github_oauth_client_secret")?.ToString();
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
            var paramString = JsonConvert.SerializeObject(param);
            Logger.Debug("GetAccessToken 请求入参：" + paramString);
            var response = HttpUtil.Post("https://github.com/login/oauth/access_token", paramString);
            Logger.Debug("GetAccessToken 返回参数：" + response);
            var data = new GithubAccessToken();
            var arr = response.Split("&");
            foreach (var item in arr)
            {
                var key = item.Split("=")[0];
                var value = item.Split("=")[1];
                data.GetType().GetProperty(key).SetValue(data, value);
            }
            return data;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public GithubUserInfo GetUserInfo(GithubAccessToken model)
        {
            var token = $"{model.token_type} {model.access_token}";
            Logger.Debug("GetUserInfo 请求头：" + token);
            var headers = new Dictionary<string, string> { { "Authorization", token } };
            var response = HttpUtil.Get("https://api.github.com/user", headers);
            var data = JsonConvert.DeserializeObject<GithubUserInfo>(response);
            Logger.Debug("GetUserInfo 返回参数：" + response);
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
                var role = new SysRoleService(Manager).GetGuest();
                var id = Guid.NewGuid().ToString();
                var avatarId = DownloadImage(githubUser.avatar_url, id);
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

        private string DownloadImage(string url, string objectid)
        {
            var result = HttpUtil.DownloadImage(url, out var contentType);
            var stream = StreamUtil.BytesToStream(result);
            var hash_code = SHAUtil.GetFileSHA1(stream);

            var config = StoreConfig.Config;
            var id = Guid.NewGuid().ToString();
            var fileName = $"{id}.jpg";
            ServiceContainer.Resolve<IStoreStrategy>(config?.Type).Upload(stream, fileName, out var filePath);

            var data = new sys_file()
            {
                id = id,
                name = fileName,
                real_name = fileName,
                hash_code = hash_code,
                file_type = "avatar",
                content_type = contentType,
                objectId = objectid
            };
            return Manager.Create(data);
        }
    }
}
