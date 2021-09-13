using Blog.Core.Auth;
using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Module.MessageRemind
{
    public class MessageRemindService : EntityService<message_remind>
    {
        #region 构造函数
        public MessageRemindService()
        {
            _context = new EntityContext<message_remind>();
        }

        public MessageRemindService(IPersistBroker broker)
        {
            _context = new EntityContext<message_remind>(broker);
        }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            return new List<EntityView>()
            {
                new EntityView()
                {
                    ViewId = "9E778EBC-9961-4CF7-B352-36DF30F33735",
                    Name = "评论消息",
                    Sql = @"
SELECT * FROM message_remind
WHERE message_type IN ('comment', 'reply')",
                    OrderBy = "createdon DESC"
                },
                new EntityView()
                {
                    ViewId = "9E778EBC-9961-4CF7-B352-36DF30F33735",
                    Name = "点赞消息",
                    Sql = @"
SELECT * FROM message_remind
WHERE message_type = 'upvote'",
                    OrderBy = "createdon DESC"
                },
            };
        }

        public override DataModel<message_remind> GetDataList(IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            searchList.Add(new SearchCondition() { Name = "receiverid", Type = SearchType.Equals, Value = UserIdentityUtil.GetCurrentUserId() });
            return base.GetDataList(searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
        }

        public override IList<message_remind> GetDataList(IList<SearchCondition> searchList, string orderBy, string viewId = "", string searchValue = "")
        {
            searchList.Add(new SearchCondition() { Name = "receiverid", Type = SearchType.Equals, Value = UserIdentityUtil.GetCurrentUserId() });
            return base.GetDataList(searchList, orderBy, viewId, searchValue);
        }

        /// <summary>
        /// 获取未读消息数量
        /// </summary>
        /// <returns></returns>
        public int GetUnReadMessageCount()
        {
            var userid = UserIdentityUtil.GetCurrentUserId();
            var result = Broker.ExecuteScalar("SELECT COUNT(1) FROM message_remind WHERE receiverid = @id AND is_read = 0", new Dictionary<string, object>() { { "@id", userid } });
            return result == null ? 0 : Convert.ToInt32(result);
        }
    }
}
