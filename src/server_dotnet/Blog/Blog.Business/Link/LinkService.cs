using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Link
{
    public class LinkService : EntityService<link>
    {
        #region 构造函数
        public LinkService()
        {
            this._context = new EntityContext<link>();
        }

        public LinkService(IPersistBroker broker)
        {
            this._context = new EntityContext<link>(broker);
        }
        #endregion
    }
}
