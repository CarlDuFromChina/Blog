using Blog.Core.Auth;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Config;
using Blog.Core.Module.Vertification.Mail;
using Blog.Core.Store;
using Sixpence.Common.Utils;
using Jdenticon.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Sixpence.Common;
using Blog.Core.Profiles;
using Sixpence.Common.Current;
using Sixpence.Common.IoC;
using Sixpence.ORM.EntityManager;
using Blog.Core.Module.System;
using Blog.Core.Module.System.Models;
using Blog.Core.Auth.Github;
using Blog.Core.Auth.Gitee;

namespace Blog.Core.Module.DataService
{
    public class SystemService
    {
        private IEntityManager Manager;

        #region 构造函数
        public SystemService()
        {
            Manager = EntityManagerFactory.GetManager();
        }

        public SystemService(IEntityManager manager)
        {
            Manager = manager;
        }
        #endregion

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
            var Manager = EntityManagerFactory.GetManager();
            var user = Manager.QueryFirst<user_info>(id);
            if (!string.IsNullOrEmpty(user?.avatar))
            {
                var config = StoreConfig.Config;
                return ServiceContainer.Resolve<IStoreStrategy>(config?.Type).DownLoad(user.avatar)?.Result;
            }
            return IdenticonResult.FromValue(id, 64);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public LoginResponse Login(LoginRequest model)
        {
            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetSystem());

            // 联合第三方登录
            if (model.third_party_login != null)
            {
                return ServiceContainer
                    .Resolve<IThirdPartyLoginStrategy>(name => name.Contains(model.third_party_login.type.ToString(), StringComparison.OrdinalIgnoreCase))
                    .Login(model.third_party_login.param);
            }

            var code = model.code;
            var pwd = model.password;
            var publicKey = model.publicKey;

            var authUser = Manager.QueryFirst<auth_user>("SELECT * FROM auth_user WHERE lower(code) = lower(@code)", new Dictionary<string, object>() { { "@code", code } });

            if (authUser == null)
            {
                return new LoginResponse() { result = false, message = "用户名或密码错误" };
            }

            if (authUser.is_lock)
            {
                return new LoginResponse() { result = false, message = "用户已被锁定，请联系管理员" };
            }

            if (string.IsNullOrEmpty(pwd) ||
                string.IsNullOrEmpty(publicKey) ||
                !string.Equals(authUser.password, RSAUtil.Decrypt(pwd, publicKey))
                )
            {
                var message = "用户名或密码错误";
                if (!authUser.try_times.HasValue)
                {
                    authUser.try_times = 1;
                }
                else
                {
                    authUser.try_times += 1;
                    if (authUser.try_times > 1)
                    {
                        message = $"用户名或密码已连续错误{authUser.try_times}次，超过五次账号锁定";
                    }
                }

                if (authUser.try_times >= 5)
                {
                    authUser.is_lock = true;
                    message = $"用户已被锁定，请联系管理员";
                }

                Manager.Update(authUser);
                return new LoginResponse() { result = false, message = message };
            }

            if (authUser.try_times > 0)
            {
                authUser.try_times = 0;
            }
            authUser.last_login_time = DateTime.Now;
            Manager.Update(authUser);

            // 返回登录结果、用户信息、用户验证票据信息
            var oUser = new LoginResponse
            {
                result = true,
                userName = code,
                token = JwtHelper.CreateToken(new JwtTokenModel() { Code = authUser.code, Name = authUser.name, Role = authUser.code, Uid = authUser.id }),
                userId = authUser.user_infoid,
                message = "登录成功"
            };
            return oUser;
        }

        /// <summary>
        /// 获取登录参数
        /// </summary>
        /// <returns></returns>
        public LoginConfig GetLoginConfig()
        {
            var github = new GithubAuthService(Manager).GetConfig();
            var gitee = new GiteeAuthService(Manager).GetConfig();
            return new LoginConfig()
            {
                github = github,
                gitee = gitee
            };
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public LoginResponse Signup(LoginRequest model)
        {
            AssertUtil.CheckIsNullOrEmpty<SpException>(model.code, "账号不能为空", "");
            AssertUtil.CheckIsNullOrEmpty<SpException>(model.password, "密码不能为空", "");

            return Manager.ExecuteTransaction(() =>
            {
                if (!model.code.Contains("@"))
                    return new LoginResponse(false, "注册失败，请使用邮箱作为账号");

                var vertification = new MailVertificationService(Manager).GetDataByMailAdress(model.code);
                if (vertification != null)
                    return new LoginResponse(false, "激活邮件已发送，请前往邮件激活账号，请勿重复注册", LoginMesageLevel.Warning);

                var id = Guid.NewGuid().ToString();
                model.password = RSAUtil.Decrypt(model.password, model.publicKey);
                var data = new mail_vertification()
                {
                    id = id,
                    name = "账号激活邮件",
                    content = $@"你好,<br/><br/>
请在两小时内点击该<a href=""{ SystemConfig.Config.Protocol }://{SystemConfig.Config.Domain}/api/MailVertification/ActivateUser?id={id}"">链接</a>激活，失效请重新登录注册
",
                    expire_time = DateTime.Now.AddHours(2),
                    is_active = false,
                    login_request = JsonConvert.SerializeObject(model),
                    mail_address = model.code,
                    mail_type = MailType.Activation.ToString()
                };
                Manager.Create(data);

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
        /// 登录或注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public LoginResponse SignInOrSignUp(LoginRequest model)
        {
            var resp = Login(model);
            if (resp.result)
                return resp;

            return Signup(model);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        public void EditPassword(string password)
        {
            var sql = $@"
UPDATE auth_user
SET password = @password
WHERE user_infoid = @id;
";
            var user = UserIdentityUtil.GetCurrentUser();
            var paramList = new Dictionary<string, object>() { { "@id", user.Id }, { "@password", password } };
            Manager.Execute(sql, paramList);
        }

        /// <summary>
        /// 充值密码
        /// </summary>
        /// <param name="id"></param>
        public void ResetPassword(string id)
        {
            var sql = $@"
UPDATE auth_user
SET password = @password
WHERE user_infoid = @id;
";
            var paramList = new Dictionary<string, object>() { { "@id", id }, { "@password", SystemConfig.Config.DefaultPassword } };
            Manager.Execute(sql, paramList);
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="code"></param>
        public void ForgetPassword(string code)
        {
            var user = Manager.QueryFirst<user_info>("SELECT * FROM user_info WHERE code = @mail OR mailbox = @mail", new Dictionary<string, object>() { { "@mail", code } });
            AssertUtil.CheckNull<SpException>(user, "用户不存在", "5E507D9C-47BC-4586-880D-D9E42D02FEA4");
            UserIdentityUtil.SetCurrentUser(MapperHelper.Map<CurrentUserModel>(user));
            var id = Guid.NewGuid().ToString();
            var sms = new mail_vertification()
            {
                id = id,
                name = "重置密码",
                content = $@"你好,<br/><br/>
请在两小时内点击该<a href=""{ SystemConfig.Config.Protocol }://{SystemConfig.Config.Domain}/api/MailVertification/ResetPassword?id={id}"">链接</a>重置密码
",
                expire_time = DateTime.Now.AddHours(2),
                is_active = false,
                mail_address = user.mailbox,
                mail_type = MailType.ResetPassword.ToString()
            };
            Manager.Create(sms);
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

            var user = Manager.QueryFirst<user_info>(userId);
            if (user == null)
                return false;

            if (user.roleid != UserIdentityUtil.ANONYMOUS_ID)
                return true;

            return false;
        }
    }
}