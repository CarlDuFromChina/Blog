using Blog.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.FocusUser
{
    public class WeChatUserController : EntityBaseController<wechat_user, WeChatUserService>
    {
        public FocusUserListModel GetFocusUserList()
        {
            return new FocusUserService().GetFocusUserList();
        }
    }
}
