using Blog.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.WeChatReply.Focus
{
    public class WeChatFocusReplyController : EntityBaseController<wechat_focus_reply, WeChatFocusReplyService>
    {
        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="id"></param>
        public void Activate(string id)
        {
            new WeChatFocusReplyService().Activate(id);
        }

        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="id"></param>
        public void Deactivate(string id)
        {
            new WeChatFocusReplyService().Deactivate(id);
        }

        public wechat_focus_reply GetData()
        {
            return new WeChatFocusReplyService().GetData();
        }
    }
}
