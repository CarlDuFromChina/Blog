using Blog.Core.Auth.Gitee.Model;
using Blog.Core.Config;
using Blog.Core.Module.SysConfig;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using log4net;
using Newtonsoft.Json;
using Sixpence.Common.IoC;
using Sixpence.Common.Logging;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth.Gitee
{
    public class GiteeAuthService
    {
        protected IEntityManager Manager;
        protected ILog Logger => LoggerFactory.GetLogger("gitee");

        public GiteeAuthService()
        {
            Manager = EntityManagerFactory.GetManager();
        }
        public GiteeAuthService(IEntityManager manager)
        {
            Manager = manager;
        }

        public GiteeConfig GetConfig()
        {
            var configService = new SysConfigService(Manager);
            var data = configService.GetValue("gitee_oauth")?.ToString();
            return JsonConvert.DeserializeObject<GiteeConfig>(data);
        }

        public GiteeAccessToken GetAccessToken(string code, string userid = "")
        {
            var config = GetConfig();
            var client_id = config.client_id;
            var client_secret = config.client_secret;
            var redirect_uri = $"{SystemConfig.Config.Protocol}://{SystemConfig.Config.Domain}/gitee-oauth";
            if (!string.IsNullOrEmpty(userid))
            {
                redirect_uri += $"?id={userid}";
            }

            Logger.Debug("GetAccessToken 请求入参：" + $"grant_type=authorization_code&code={code}&client_id={client_id}&client_secret={client_secret}&redirect_uri={redirect_uri}");
            var response = HttpUtil.Post($"https://gitee.com/oauth/token?grant_type=authorization_code&code={code}&client_id={client_id}&client_secret={client_secret}&redirect_uri={redirect_uri}", "");
            Logger.Debug("GetAccessToken 返回参数：" + response);
            return JsonConvert.DeserializeObject<GiteeAccessToken>(response);
        }

        public GiteeUserInfo GetGiteeUserInfo(GiteeAccessToken model)
        {
            Logger.Debug("GetGiteeUserInfo 请求参数：" + model.access_token);
            var response = HttpUtil.Get("https://gitee.com/api/v5/user?access_token=" + model.access_token);
            var data = JsonConvert.DeserializeObject<GiteeUserInfo>(response);
            Logger.Debug("GetGiteeUserInfo 返回参数：" + response);
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
            var fileName = $"{id}.png";
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
