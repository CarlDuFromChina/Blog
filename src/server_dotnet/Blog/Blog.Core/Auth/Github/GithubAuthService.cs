using Blog.Core.Auth.Github.Model;
using Blog.Core.Config;
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

namespace Blog.Core.Auth.Github
{
    public class GithubAuthService
    {
        protected IEntityManager Manager;
        protected ILog Logger => LoggerFactory.GetLogger("github");

        #region 构造函数
        public GithubAuthService()
        {
            Manager = EntityManagerFactory.GetManager();
        }

        public GithubAuthService(IEntityManager manager)
        {
            Manager = manager;
        }
        #endregion

        public GithubConfig GetConfig()
        {
            var configService = new SysConfigService(Manager);
            var data = configService.GetValue("github_oauth")?.ToString();
            return JsonConvert.DeserializeObject<GithubConfig>(data);
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
        /// 下载头像
        /// </summary>
        /// <param name="url"></param>
        /// <param name="objectid"></param>
        /// <returns></returns>
        public string DownloadImage(string url, string objectid)
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
