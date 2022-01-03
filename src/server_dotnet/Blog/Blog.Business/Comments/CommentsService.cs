using Blog.Core;
using Sixpence.ORM.Entity;
using Sixpence.Common;
using System.Collections.Generic;
using System.Linq;
using Sixpence.ORM.Broker;
using Sixpence.ORM.Models;

namespace Blog.Comments
{
    public class CommentsService : EntityService<comments>
    {
        #region 构造函数
        public CommentsService()
        {
            this._context = new EntityContext<comments>();
        }
        public CommentsService(IPersistBroker broker)
        {
            this._context = new EntityContext<comments>(broker);
        }
        #endregion

        public override IList<comments> GetDataList(IList<SearchCondition> searchList, string orderBy, string viewId = "", string searchValue = "")
        {
            var comments = base.GetDataList(searchList, orderBy, viewId, searchValue).ToList();
            var dataList = comments.Where(item => string.IsNullOrEmpty(item.parentid));
            dataList.Each(item => item.Comments = comments.Where(e => e.parentid == item.Id));
            return dataList.ToList();
        }
    }
}
