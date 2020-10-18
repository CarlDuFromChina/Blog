using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.WeChatNews
{
    [RequestAuthorize]
    public class WeChatNewsController : EntityBaseController<wechat_news, WeChatNewsService>
    {
    }
}
