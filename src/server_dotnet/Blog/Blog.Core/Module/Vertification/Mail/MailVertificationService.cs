using Blog.Core.Auth;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Data;
using Blog.Core.Module.Role;
using Blog.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.Vertification.Mail
{
    public class MailVertificationService : EntityService<mail_vertification>
    {
        #region 构造函数
        public MailVertificationService()
        {
            _context = new EntityContext<mail_vertification>();
        }

        public MailVertificationService(IPersistBroker broker)
        {
            _context = new EntityContext<mail_vertification>(broker);
        }
        #endregion

        /// <summary>
        /// 查询未激活且未过期的邮件
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public mail_vertification GetDataByMailAdress(string mail)
        {
            var sql = @"SELECT * FROM mail_vertification
WHERE mail_address = @address
AND is_active = 0
AND expire_time > CURRENT_TIMESTAMP";
            return Broker.Retrieve<mail_vertification>(sql, new Dictionary<string, object>() { { "@address", mail } });
        }

        /// <summary>
        /// 邮箱激活用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ActivateUser(string id)
        {
            return Broker.ExecuteTransaction(() =>
            {
                var data = GetData(id);
                if (data == null)
                    return "激活失败";

                if (data.expire_time < DateTime.Now)
                    return "激活失败，激活链接已过期";

                #region 创建用户
                var model = JsonConvert.DeserializeObject<LoginRequest>(data.login_request.ToString());
                var role = new SysRoleService(Broker).GetGuest();
                var user = new user_info()
                {
                    Id = Guid.NewGuid().ToString(),
                    code = model.code,
                    password = model.password,
                    name = model.code.Split("@")[0],
                    roleid = role.Id,
                    roleidName = role.name,
                    stateCode = 1,
                    stateCodeName = "启用"
                };
                Broker.Create(user, false);
                var _authUser = new auth_user()
                {
                    Id = user.user_infoId,
                    name = user.name,
                    code = user.code,
                    roleid = user.roleid,
                    roleidName = user.roleidName,
                    user_infoid = user.user_infoId,
                    is_lock = false,
                    is_lockName = "否",
                    last_login_time = DateTime.Now,
                    password = model.password
                };
                Broker.Create(_authUser);
                #endregion

                data.is_active = true;
                Broker.Update(data);

                return "激活成功";
            });
        }
    }
}
