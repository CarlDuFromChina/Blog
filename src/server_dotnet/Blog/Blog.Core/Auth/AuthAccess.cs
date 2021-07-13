using Blog.Core.Auth.Privilege;
using Blog.Core.Auth.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Auth
{
    /// <summary>
    /// 权限检查
    /// </summary>
    public static class AuthAccess
    {
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        private static bool CheckAccess(string entityId, OperationType operationType, string userId)
        {
            var data = UserPrivilegesCache.GetUserPrivileges(string.IsNullOrEmpty(userId) ? UserIdentityUtil.GetCurrentUser()?.Id : userId)
                .Where(item => item.sys_entityid == entityId)
                .FirstOrDefault();
            return data != null && (data.privilege & (int)operationType) == (int)operationType;
        }

        /// <summary>
        /// 检查实体读权限
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CheckReadAccess(string entityId, string userId = "")
        {
            return CheckAccess(entityId, OperationType.Read, userId);
        }

        /// <summary>
        /// 检查实体写权限
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CheckWriteAccess(string entityId, string userId = "")
        {
            return CheckAccess(entityId, OperationType.Write, userId);
        }

        /// <summary>
        /// 检查实体删权限
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CheckDeleteAccess(string entityId, string userId = "")
        {
            return CheckAccess(entityId, OperationType.Delete, userId);
        }
    }
}
