using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Module.MessageRemind
{
    public class MessageRemindController : EntityBaseController<message_remind, MessageRemindService>
    {
        /// <summary>
        /// 获取未读消息数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public int GetUnReadMessageCount()
        {
            return new MessageRemindService().GetUnReadMessageCount();
        }
    }
}
