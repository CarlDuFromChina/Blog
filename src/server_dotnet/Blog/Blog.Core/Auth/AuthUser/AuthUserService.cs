using Blog.Core.Config;
using Sixpence.ORM.Entity;
using Sixpence.Common;
using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.Web;
using Sixpence.ORM.EntityManager;

namespace Blog.Core.Auth
{
    public class AuthUserService : EntityService<auth_user>
    {
        #region 构造函数
        public AuthUserService() : base() { }

        public AuthUserService(IEntityManager manger) : base(manger) { }
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
            var authUser = Manager.QueryFirst<auth_user>(sql, paramList);
            return authUser;
        }
        public auth_user GetDataByCode(string code)
        {
            var data = Manager.QueryFirst<auth_user>("select * from auth_user where code = @code", new Dictionary<string, object>() { { "@code", code } });
            return data;
        }

        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="id"></param>
        public void LockUser(string id)
        {
            Manager.ExecuteTransaction(() =>
            {
                var userId = UserIdentityUtil.GetCurrentUserId();
                AssertUtil.CheckBoolean<SpException>(userId == id, "请勿锁定自己", "4B1DD6F4-977B-43B4-BA48-C02668A661B3");
                var data = Manager.QueryFirst<auth_user>("select * from auth_user where user_infoid = @id", new Dictionary<string, object>() { { "@id", id } });
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
            Manager.ExecuteTransaction(() =>
            {
                var data = Manager.QueryFirst<auth_user>("select * from auth_user where user_infoid = @id", new Dictionary<string, object>() { { "@id", id } });
                data.is_lock = false;
                UpdateData(data);
            });
        }
    }
}