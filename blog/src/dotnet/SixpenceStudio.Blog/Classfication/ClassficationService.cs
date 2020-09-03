using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Classfication
{
    public class ClassficationService : EntityService<classfication>
    {
        #region 构造函数
        public ClassficationService()
        {
            this._cmd = new EntityCommand<classfication>();
        }

        public ClassficationService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<classfication>(broker);
        }
        #endregion
    }
}
