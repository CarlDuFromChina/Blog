using Sixpence.Common.Current;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Auth
{
    /// <summary>
    /// 用户身份认证帮助类
    /// </summary>
    public static class UserIdentityUtil
    {
        /// <summary>
        /// 匿名id
        /// </summary>
        public const string ANONYMOUS_ID = "222222222-22222-2222-2222-222222222222";

        /// <summary>
        /// 管理员id
        /// </summary>
        public const string ADMIN_ID = "00000000-0000-0000-0000-000000000000";

        /// <summary>
        /// 系统用户id
        /// </summary>
        public const string SYSTEM_ID = "111111111-11111-1111-1111-111111111111";

        /// <summary>
        /// 用户id
        /// </summary>
        public const string USER_ID = "333333333-33333-3333-3333-333333333333";

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
                Id = ADMIN_ID,
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
                Id = ANONYMOUS_ID,
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
                Id = SYSTEM_ID,
                Name = "系统"
            };
        }

        /// <summary>
        /// 是否权限等级高于对方
        /// </summary>
        /// <param name="currentid"></param>
        /// <param name="compareId"></param>
        /// <returns></returns>
        public static bool IsOwner(string currentid, string compareId)
        {
            var dictionary = new Dictionary<string, int>()
            {
                { ADMIN_ID, 1 },
                { SYSTEM_ID, 2 },
                { USER_ID,4 },
                { ANONYMOUS_ID, 9 }
            };
            return dictionary[currentid] <= dictionary[compareId];
        }
    }
}
