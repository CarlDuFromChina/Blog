using Blog.Core.Auth;
using Sixpence.EntityFramework.Entity;
using Sixpence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.EntityFramework.Models;
using Sixpence.EntityFramework.Broker;

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
                    ViewId = "FBAF583B-4B25-477B-B5DC-C5D110976A9A",
                    Name = "点赞消息",
                    Sql = @"
SELECT * FROM message_remind
WHERE message_type = 'upvote'",
                    OrderBy = "createdon DESC"
                },
                new EntityView()
                {
                    ViewId = "F7A3E0A9-4951-4EA7-A486-5B35056AC17A",
                    Name = "系统消息",
                    Sql = @"
SELECT * FROM message_remind
WHERE message_type = 'system'",
                    OrderBy = "createdon DESC"
                },
            };
        }

        public void ReadMessage(IEnumerable<string> ids)
        {
            var sql = @"
UPDATE message_remind
SET	is_read = 1, is_readname = '是'
WHERE message_remindid IN (in@ids)
";
            Broker.Execute(sql, new Dictionary<string, object>() { { "in@ids", string.Join(",", ids) } });
        }

        public override DataModel<message_remind> GetDataList(IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            if (searchList.IsEmpty())
            {
                searchList = new List<SearchCondition>();
            }
            searchList.Add(new SearchCondition() { Name = "receiverid", Type = SearchType.Equals, Value = UserIdentityUtil.GetCurrentUserId() });
            var model = base.GetDataList(searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
            var ids = model.DataList.Where(item => !item.is_read).Select(item => item.Id);
            ReadMessage(ids);
            return model;
        }

        public override IList<message_remind> GetDataList(IList<SearchCondition> searchList, string orderBy, string viewId = "", string searchValue = "")
        {
            if (searchList.IsEmpty())
            {
                searchList = new List<SearchCondition>();
            }
            searchList.Add(new SearchCondition() { Name = "receiverid", Type = SearchType.Equals, Value = UserIdentityUtil.GetCurrentUserId() });
            var model = base.GetDataList(searchList, orderBy, viewId, searchValue);
            var ids = model.Where(item => !item.is_read).Select(item => item.Id);
            ReadMessage(ids);
            return model;
        }

        /// <summary>
        /// 获取未读消息数量
        /// </summary>
        /// <returns></returns>
        public object GetUnReadMessageCount()
        {
            var userid = UserIdentityUtil.GetCurrentUserId();
            var paramList = new Dictionary<string, object>() { { "@id", userid } };
            var sql = @"
SELECT COUNT(1)
FROM message_remind
WHERE receiverid = @id AND is_read = 0";
            var total = Broker.ExecuteScalar(sql, paramList);
            var upvote = Broker.ExecuteScalar($"{sql} AND message_type = 'upvote'", paramList);
            var comment = Broker.ExecuteScalar($"{sql} AND message_type IN ('comment', 'reply')", paramList);
            var system = Broker.ExecuteScalar($"{sql} AND message_type = 'system'", paramList);
            return new
            {
                total = Convert.ToInt32(total),
                upvote = Convert.ToInt32(upvote),
                comment = Convert.ToInt32(comment),
                system = Convert.ToInt32(system)
            };
        }
    }
}
