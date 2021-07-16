using Blog.Core.Data;

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
    }
}
