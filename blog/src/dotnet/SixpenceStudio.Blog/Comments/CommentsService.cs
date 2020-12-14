using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;

namespace SixpenceStudio.Blog.Comments
{
    public class CommentsService : EntityService<comments>
    {
        #region 构造函数
        public CommentsService()
        {
            this._cmd = new EntityCommand<comments>();
        }
        public CommentsService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<comments>(broker);
        }
        #endregion
    }
}
