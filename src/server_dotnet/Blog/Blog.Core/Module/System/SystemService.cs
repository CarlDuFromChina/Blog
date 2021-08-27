using Blog.Core;
using Blog.Core.Auth;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Config;
using Blog.Core.Data;
using Blog.Core.Module.Role;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using Blog.Core.Utils;
using Jdenticon.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public object GetAvatar(string id)
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
                return ServiceContainer.Resolve<IStoreStrategy>(config?.Type).DownLoad(user.avatar);
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
            AssertUtil.CheckBoolean<SpException>(!model.code.Contains("@"), "账号格式错误", "");
            AssertUtil.CheckIsNullOrEmpty<SpException>(model.password, "密码不能为空", "");

            return Broker.ExecuteTransaction(() =>
            {
                var authUser = Broker.Retrieve<auth_user>("SELECT * FROM auth_user WHERE lower(code) = lower(@code)", new Dictionary<string, object>() { { "@code", model.code } });
                if (authUser != null)
                {
                    return new AuthUserService(Broker).Login(model.code, model.password, model.publicKey);
                }

                var role = new SysRoleService().GetGuest();
                var user = new user_info()
                {
                    Id = Guid.NewGuid().ToString(),
                    code = model.code,
                    password = model.password,
                    name = model.code.Split("@")[0],
                    roleid = role.Id,
                    roleidName = role.name,
                    stateCode = 1,
                    stateCodeName = "启用"
                };
                Broker.Create(user, false);
                var _authUser = new auth_user()
                {
                    Id = user.user_infoId,
                    name = user.name,
                    code = user.code,
                    roleid = user.roleid,
                    roleidName = user.roleidName,
                    user_infoid = user.user_infoId,
                    is_lock = false,
                    is_lockName = "否",
                    last_login_time = DateTime.Now,
                    password = RSAUtil.Decrypt(model.password, model.publicKey)
                };
                Broker.Create(_authUser);

                // 返回登录结果、用户信息、用户验证票据信息
                return new LoginResponse
                {
                    result = true,
                    userName = user.code,
                    token = JwtHelper.CreateToken(new JwtTokenModel() { Code = user.code, Name = user.name, Role = user.code, Uid = user.Id }),
                    userId = user.Id,
                    message = "登录成功"
                };
            });
        }
    }
}