using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.WeChatNews
{
    public class WeChatNewsService : EntityService<wechat_news>
    {
        #region 构造函数
        public WeChatNewsService()
        {
            this._cmd = new EntityCommand<wechat_news>();
        }

        public WeChatNewsService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<wechat_news>(broker);
        }
        #endregion
    }
}
