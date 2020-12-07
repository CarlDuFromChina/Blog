using SixpenceStudio.Core;
using SixpenceStudio.Core.AuthUser;
using SixpenceStudio.Core.UserInfo;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using SixpenceStudio.Core.Auth;

namespace SixpenceStudio.Blog.Comments
{
    public class CommentsService : EntityService<comments>
    {
        #region 构造函数
        public CommentsService()
        {
            this._cmd = new EntityCommand<comments>();
        }
        public CommentsService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<comments>(broker);
        }
        #endregion

        public override string CreateData(comments t)
        {
            var user = UserIdentityUtil.GetAnonymous();
            t.modifiedBy = user.Id;
            t.modifiedByName = user.Name;
            t.createdBy = user.Id;
            t.createdByName = user.Name;
            t.createdOn = DateTime.Now;
            t.modifiedOn = DateTime.Now;
            return base.CreateData(t);
        }
    }
}
