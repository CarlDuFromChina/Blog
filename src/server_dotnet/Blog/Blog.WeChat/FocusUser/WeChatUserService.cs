﻿using Blog.Core.WebApi;
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.FocusUser
{
    public class WeChatUserService : EntityService<wechat_user>
    {
        #region 构造函数
        public WeChatUserService()
        {
            _context = new EntityContext<wechat_user>();
        }
       
        public WeChatUserService(IPersistBroker broker)
        {
            _context = new EntityContext<wechat_user>(broker);
        }
        #endregion
    }
}