using Blog.Core.WebApi;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
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
        public WeChatUserService() : base() { }
       
        public WeChatUserService(IEntityManager manager) : base(manager) { }
        #endregion
    }
}
