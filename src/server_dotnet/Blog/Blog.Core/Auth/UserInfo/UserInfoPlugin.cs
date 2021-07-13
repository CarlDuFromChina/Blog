using Blog.Core.Config;
using Blog.Core.Data;
using Blog.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Auth.UserInfo
{
    public class UserInfoPlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    CreateAuthInfo(context.Entity, context.Broker);
                    break;
                case EntityAction.PostUpdate:
                    UpdateAuthInfo(context.Entity, context.Broker);
                    break;
                case EntityAction.PostDelete:
                    DeleteAuthInfo(context.Entity, context.Broker);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 创建用户认证信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        private void CreateAuthInfo(BaseEntity entity, IPersistBroker broker)
        {
            var authInfo = new auth_user()
            {
                auth_userId = entity.GetAttributeValue<string>("user_infoId"),
                name = entity.GetAttributeValue<string>("name"),
                code = entity.GetAttributeValue<string>("code"),
                password = SystemConfig.Config.DefaultPassword,
                user_infoid = entity.GetAttributeValue<string>("user_infoId"),
                roleid = entity.GetAttributeValue<string>("roleid"),
                roleidName = entity.GetAttributeValue<string>("roleidName"),
                is_lock = false,
                is_lockName = "否"
            };
            new AuthUserService(broker).CreateData(authInfo);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        private void UpdateAuthInfo(BaseEntity entity, IPersistBroker broker)
        {
            var sql = @"
SELECT * FROM auth_user
WHERE user_infoid = @id
";
            var authInfo = broker.Retrieve<auth_user>(sql, new Dictionary<string, object>() { { "@id", entity["user_infoId"]?.ToString() } });
            AssertUtil.CheckNull<SpException>(authInfo, "用户Id不能为空", "C37CCF94-6B27-4BF4-AF29-DBEDC9E53E5D");
            authInfo.name = entity["name"]?.ToString();
            new AuthUserService(broker).UpdateData(authInfo);
        }

        /// <summary>
        /// 删除用户认证信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        private void DeleteAuthInfo(BaseEntity entity, IPersistBroker broker)
        {
            var sql = @"
DELETE FROM auth_user WHERE user_infoid = @id
";
            broker.Execute(sql, new Dictionary<string, object>() { { "@id", entity["user_infoId"]?.ToString() } });
        }

    }
}