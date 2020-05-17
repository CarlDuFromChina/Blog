using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.RecommendInfo
{
    public class RecommendInfoService : EntityService<recommend_info>
    {
        #region 构造函数
        public RecommendInfoService()
        {
            _cmd = new EntityCommand<recommend_info>();
        }

        public RecommendInfoService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<recommend_info>(broker);
        }
        #endregion


    }
}
