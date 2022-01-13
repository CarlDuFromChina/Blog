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
                id = entity.GetAttributeValue<string>("user_infoId"),
                name = entity.GetAttributeValue<string>("name"),
                code = entity.GetAttributeValue<string>("code"),
                password = SystemConfig.Config.DefaultPassword,
                user_infoid = entity.GetAttributeValue<string>("user_infoId"),
                roleid = entity.GetAttributeValue<string>("roleid"),
                roleidName = entity.GetAttributeValue<string>("roleidName"),
                is_lock = false,
                is_lockName = "否"
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
            var authInfo = manager.QueryFirst<auth_user>(sql, new Dictionary<string, object>() { { "@id", entity["user_infoId"]?.ToString() } });
            AssertUtil.CheckNull<SpException>(authInfo, "用户Id不能为空", "C37CCF94-6B27-4BF4-AF29-DBEDC9E53E5D");
            authInfo.name = entity["name"]?.ToString();
            authInfo.roleid = entity["roleid"]?.ToString();
            authInfo.roleidName = entity["roleidName"]?.ToString();
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
            AssertUtil.CheckBoolean<SpException>(!allowUpdateRole, $"你没有权限修改角色为[{entity["roleidName"]}]", "2ABD2CBA-A7CB-4F61-841F-7CD4E6C1BD69");
            AssertUtil.CheckBoolean<SpException>(entity.PrimaryKey.Value == "00000000-0000-0000-0000-000000000000", "系统管理员信息禁止更新", "");
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
            manager.Execute(sql, new Dictionary<string, object>() { { "@id", entity["user_infoId"]?.ToString() } });
        }

    }
}