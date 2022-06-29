using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.FocusUser
{
    public class WeChatUserController : EntityBaseController<wechat_user, WeChatUserService>
    {
        [HttpGet("focus_user")]
        public FocusUserListModel GetFocusUserList()
        {
            return new FocusUserService().GetFocusUserList();
        }
    }
}
