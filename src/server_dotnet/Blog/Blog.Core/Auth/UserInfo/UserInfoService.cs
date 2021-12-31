using Sixpence.EntityFramework.Entity;
using Sixpence.Common;
using Sixpence.Common.Utils;
using System.Collections.Generic;
using Sixpence.EntityFramework.Broker;

namespace Blog.Core.Auth.UserInfo
{
    public class UserInfoService : EntityService<user_info>
    {
        #region 构造函数
        public UserInfoService()
        {
            this._context = new EntityContext<user_info>();
        }

        public UserInfoService(IPersistBroker broker)
        {
            this._context = new EntityContext<user_info>(broker);
        }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            var sql = @"
SELECT
    is_lockName,
	user_info.*
FROM
	user_info
LEFT JOIN (
    SELECT
        user_infoid,
        is_lockName
    FROM auth_user
) au ON user_info.user_infoid = au.user_infoid
";
            var customFilter = new List<string>() { "name" };
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = sql,
                    CustomFilter = customFilter,
                    OrderBy = "name, createdon",
                    ViewId = "59F908EB-A353-4205-ABE4-FA9DB27DD434",
                    Name = "所有的用户信息"
                }
            };
        }

        public user_info GetData()
        {
            return Broker.Retrieve<user_info>(UserIdentityUtil.GetCurrentUserId());
        }

        /// <summary>
        /// 通过Code查询用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public user_info GetDataByCode(string code)
        {
            var sql = @"
SELECT * FROM user_info
WHERE code = @code";
            return Broker.Retrieve<user_info>(sql, new Dictionary<string, object>() { { "@code", code } });
        }

        /// <summary>
        /// 是否需要填充信息
        /// </summary>
        /// <returns></returns>
        public bool InfoFilled()
        {
            var user = Broker.Retrieve<user_info>(UserIdentityUtil.GetCurrentUserId());
            AssertUtil.CheckNull<SpException>(user, "未查询到用户", "BE999374-F0CF-4274-8D9D-1E436FBA6935");
            if (user.Id == UserIdentityUtil.ADMIN_ID)
            {
                return false;
            }
            return !user.gender.HasValue || AssertUtil.CheckEmpty(user.mailbox, user.cellphone, user.realname);
        }
    }
}
