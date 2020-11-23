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
            // 匿名
            if (string.IsNullOrEmpty(_cmd.Broker.GetCurrentUser().Id))
            {
                var anonymous = _cmd.Broker.Retrieve<user_info>("select * from user_info where code = 'Anonymous'", null);
                t.modifiedBy = anonymous.Id;
                t.modifiedByName = anonymous.name;
                t.createdBy = anonymous.Id;
                t.createdByName = anonymous.name;
                t.createdOn = DateTime.Now;
                t.modifiedOn = DateTime.Now;
                return _cmd.Broker.Create(t);
            }
            return base.CreateData(t);
        }

        public override void UpdateData(comments t)
        {
            // 匿名
            if (string.IsNullOrEmpty(_cmd.Broker.GetCurrentUser().Id))
            {
                var anonymous = _cmd.Broker.Retrieve<user_info>("select * from user_info where code = 'Anonymous'", null);
                t.createdBy = anonymous.Id;
                t.createdByName = anonymous.name;
                t.modifiedOn = DateTime.Now;
                _cmd.Broker.Update(t);
                return;
            }
            base.UpdateData(t);
        }
    }
}
