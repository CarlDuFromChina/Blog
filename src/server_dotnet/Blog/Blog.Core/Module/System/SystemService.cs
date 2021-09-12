using Blog.Core;
using Blog.Core.Auth;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Config;
using Blog.Core.Data;
using Blog.Core.Module.Role;
using Blog.Core.Module.Vertification.Mail;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using Blog.Core.Utils;
using Jdenticon.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Blog.Core.Module.DataService
{
    public class SystemService
    {
        private IPersistBroker Broker = PersistBrokerFactory.GetPersistBroker();

        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        public string GetPublicKey()
        {
            return RSAUtil.GetKey();
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <returns></returns>
        public string GetRandomImage()
        {
            var result = HttpUtil.Get("https://api.ixiaowai.cn/api/api.php?return=json");
            return result;
        }

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult GetAvatar(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var broker = PersistBrokerFactory.GetPersistBroker();
            var user = broker.Retrieve<user_info>(id);
            if (!string.IsNullOrEmpty(user?.avatar))
            {
                var config = StoreConfig.Config;
                return ServiceContainer.Resolve<IStoreStrategy>(config?.Type).DownLoad(user.avatar)?.Result;
            }
            return IdenticonResult.FromValue(id, 64);
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public LoginResponse Login(LoginRequest model)
        {
            AssertUtil.CheckIsNullOrEmpty<SpException>(model.code, "账号不能为空", "");
            AssertUtil.CheckIsNullOrEmpty<SpException>(model.password, "密码不能为空", "");

            return Broker.ExecuteTransaction(() =>
            {
                var loginResponse = new AuthUserService(Broker).Login(model.code, model.password, model.publicKey);
                if (loginResponse.result)
                {
                    return loginResponse;
                }

                if (!model.code.Contains("@"))
                    return new LoginResponse(false, "注册失败，请使用邮箱作为账号");

                var vertification = new MailVertificationService(Broker).GetDataByMailAdress(model.code);
                if (vertification != null)
                    return new LoginResponse(false, "激活邮件已发送，请前往邮件激活账号，请勿重复注册", LoginMesageLevel.Warning);

                var id = Guid.NewGuid().ToString();
                model.password = RSAUtil.Decrypt(model.password, model.publicKey);
                var data = new mail_vertification()
                {
                    Id = id,
                    name = "账号激活邮件",
                    content = $@"你好,<br/><br/>
请在两小时内点击该<a href=""{ SystemConfig.Config.Protocol }://{SystemConfig.Config.Domain}/api/MailVertification/ActivateUser?id={id}"">链接</a>激活，失效请重新登录注册
",
                    expire_time = DateTime.Now.AddHours(2),
                    is_active = false,
                    login_request = JsonConvert.SerializeObject(model),
                    mail_address = model.code
                };
                Broker.Create(data);

                // 返回登录结果、用户信息、用户验证票据信息
                return new LoginResponse()
                {
                    result = false,
                    message = $"已向{data.mail_address}发送激活邮件，请在两个小时内激活",
                    level = LoginMesageLevel.Warning.ToString()
                };
            });
        }

        /// <summary>
        /// 是否有进入后台权限
        /// </summary>
        /// <returns></returns>
        public bool GetShowAdmin()
        {
            var userId = UserIdentityUtil.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return false;

            var user = Broker.Retrieve<user_info>(userId);
            if (user == null)
                return false;

            if (user.roleid != UserIdentityUtil.ANONYMOUS_ID)
                return true;

            return false;
        }
    }
}