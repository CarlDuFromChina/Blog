using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Auth
{
    /// <summary>
    /// 用户身份认证帮助类
    /// </summary>
    public class UserIdentityUtil
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public static string UserId
        {
            get
            {
                return GetCurrentUser()?.Id;
            }
        }

        /// <summary>
        /// 设置当前线程用户
        /// </summary>
        /// <param name="user"></param>
        public static void SetCurrentUser(CurrentUserModel user)
        {
            if (user != null)
            {
                CallContext<CurrentUserModel>.SetData(CallContextType.User, user);
            }
        }

        /// <summary>
        /// 获取当前线程用户
        /// </summary>
        /// <returns></returns>
        public static CurrentUserModel GetCurrentUser()
        {
            return CallContext<CurrentUserModel>.GetData(CallContextType.User);
        }

        /// <summary>
        /// 获取当前线程用户Id
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserId()
        {
            return GetCurrentUser()?.Id;
        }

        /// <summary>
        /// 获取管理员对象
        /// </summary>
        /// <returns></returns>
        public static CurrentUserModel GetAdmin()
        {
            return new CurrentUserModel()
            {
                Code = "admin",
                Id = "00000000-0000-0000-0000-000000000000",
                Name = "系统管理员"
            };
        }

        /// <summary>
        /// 获取匿名用户对象
        /// </summary>
        /// <returns></returns>
        public static CurrentUserModel GetAnonymous()
        {
            return new CurrentUserModel()
            {
                Code = "anonymous",
                Id = "222222222-22222-2222-2222-222222222222",
                Name = "访客"
            };
        }

        /// <summary>
        /// 获取系统用户
        /// </summary>
        /// <returns></returns>
        public static CurrentUserModel GetSystem()
        {
            return new CurrentUserModel()
            {
                Code = "system",
                Id = "111111111-11111-1111-1111-111111111111",
                Name = "系统"
            };
        }
    }
}
