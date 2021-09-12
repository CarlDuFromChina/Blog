using Blog.Core.Config;
using Blog.Core.Data;
using Blog.Core.Utils;
using System;
using System.Collections.Generic;
using System.Web;

namespace Blog.Core.Auth
{
    public class AuthUserService : EntityService<auth_user>
    {
        #region 构造函数
        public AuthUserService()
        {
            _context = new EntityContext<auth_user>();
        }

        public AuthUserService(IPersistBroker broker)
        {
            _context = new EntityContext<auth_user>(broker);
        }
        #endregion

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pwd">MD5密码</param>
        /// <returns></returns>
        public auth_user GetData(string code, string pwd)
        {
            var sql = @"
SELECT * FROM auth_user WHERE code = @code AND password = @password;
";
            var paramList = new Dictionary<string, object>() { { "@code", code }, { "@password", pwd } };
            var authUser = Broker.Retrieve<auth_user>(sql, paramList);
            return authUser;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public LoginResponse Login(string code, string pwd, string publicKey)
        {
            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetSystem());

            var authUser = Broker.Retrieve<auth_user>("SELECT * FROM auth_user WHERE lower(code) = lower(@code)", new Dictionary<string, object>() { { "@code", code } });

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

                Broker.Update(authUser);
                return new LoginResponse() { result = false, message = message };
            }

            if (authUser.try_times > 0)
            {
                authUser.try_times = 0;
            }
            authUser.last_login_time = DateTime.Now;
            Broker.Update(authUser);

            // 返回登录结果、用户信息、用户验证票据信息
            var oUser = new LoginResponse
            {
                result = true,
                userName = code,
                token = JwtHelper.CreateToken(new JwtTokenModel() { Code = authUser.code, Name = authUser.name, Role = authUser.code, Uid = authUser.Id }),
                userId = authUser.user_infoid,
                message = "登录成功"
            };
            return oUser;
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
            var paramList = new Dictionary<string, object>() { { "@id",  user.Id}, { "@password", password } };
            Broker.Execute(sql, paramList);
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
            Broker.Execute(sql, paramList);
        }

        public auth_user GetDataByCode(string code)
        {
            var data = Broker.Retrieve<auth_user>("select * from auth_user where code = @code", new Dictionary<string, object>(){ { "@code", code } });
            return data;
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="id"></param>
        public void LockUser(string id)
        {
            Broker.ExecuteTransaction(() =>
            {
                var userId = UserIdentityUtil.GetCurrentUserId();
                AssertUtil.CheckBoolean<SpException>(userId == id, "请勿锁定自己", "4B1DD6F4-977B-43B4-BA48-C02668A661B3");
                var data = Broker.Retrieve<auth_user>("select * from auth_user where user_infoid = @id", new Dictionary<string, object>() { { "@id", id } });
                data.is_lock = true;
                UpdateData(data);
            });
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="id"></param>
        public void UnlockUser(string id)
        {
            Broker.ExecuteTransaction(() =>
            {
                var data = Broker.Retrieve<auth_user>("select * from auth_user where user_infoid = @id", new Dictionary<string, object>() { { "@id", id } });
                data.is_lock = false;
                UpdateData(data);
            });
        }
    }
}