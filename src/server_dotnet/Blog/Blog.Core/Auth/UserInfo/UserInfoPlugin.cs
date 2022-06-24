using Blog.Core.Config;
using Sixpence.ORM.Entity;
using Blog.Core.Module.Role;
using Sixpence.Common;
using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sixpence.ORM.EntityManager;

namespace Blog.Core.Auth.UserInfo
{
    public class UserInfoPlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            switch (context.Action)
            {
                case EntityAction.PreCreate:
                case EntityAction.PreUpdate:
                    CheckUserInfo(context.Entity, context.EntityManager);
                    break;
                case EntityAction.PostCreate:
                    CreateAuthInfo(context.Entity, context.EntityManager);
                    break;
                case EntityAction.PostUpdate:
                    UpdateAuthInfo(context.Entity, context.EntityManager);
                    break;
                case EntityAction.PostDelete:
                    DeleteAuthInfo(context.Entity, context.EntityManager);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 创建用户认证信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="manager"></param>
        private void CreateAuthInfo(BaseEntity entity, IEntityManager manager)
        {
            var authInfo = new auth_user()
            {
                id = entity.GetAttributeValue<string>("id"),
                name = entity.GetAttributeValue<string>("name"),
                code = entity.GetAttributeValue<string>("code"),
                password = SystemConfig.Config.DefaultPassword,
                user_infoid = entity.GetAttributeValue<string>("id"),
                roleid = entity.GetAttributeValue<string>("roleid"),
                roleid_name = entity.GetAttributeValue<string>("roleid_name"),
                is_lock = false,
            };
            new AuthUserService(manager).CreateData(authInfo);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="manager"></param>
        private void UpdateAuthInfo(BaseEntity entity, IEntityManager manager)
        {
            var sql = @"
SELECT * FROM auth_user
WHERE user_infoid = @id
";
            var authInfo = manager.QueryFirst<auth_user>(sql, new Dictionary<string, object>() { { "@id", entity["id"]?.ToString() } });
            AssertUtil.IsNull(authInfo, "用户Id不能为空");
            authInfo.name = entity["name"]?.ToString();
            authInfo.roleid = entity["roleid"]?.ToString();
            authInfo.roleid_name = entity["roleid_name"]?.ToString();
            new AuthUserService(manager).UpdateData(authInfo);
        }

        /// <summary>
        /// 检查用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        private void CheckUserInfo(BaseEntity entity, IEntityManager manager)
        {
            var allowUpdateRole = new SysRoleService(manager).AllowCreateOrUpdateRole(entity["roleid"].ToString());
            AssertUtil.IsTrue(!allowUpdateRole, $"你没有权限修改角色为[{entity["roleid_name"]}]");
            AssertUtil.IsTrue(entity.GetPrimaryColumn().Value == "00000000-0000-0000-0000-000000000000", "系统管理员信息禁止更新");
        }

        /// <summary>
        /// 删除用户认证信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        private void DeleteAuthInfo(BaseEntity entity, IEntityManager manager)
        {
            var sql = @"
DELETE FROM auth_user WHERE user_infoid = @id
";
            manager.Execute(sql, new Dictionary<string, object>() { { "@id", entity["id"]?.ToString() } });
        }

    }
}