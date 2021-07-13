using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Classification
{
    public class ClassificationService : EntityService<classification>
    {
        #region 构造函数
        public ClassificationService()
        {
            this._cmd = new EntityCommand<classification>();
        }

        public ClassificationService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<classification>(broker);
        }
        #endregion
    }
}
