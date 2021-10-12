using Blog.Core.Auth;
using Blog.Core.Auth.UserInfo;
using Sixpence.EntityFramework.Entity;
using Blog.Core.Module.Role;
using Sixpence.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Sixpence.EntityFramework.Broker;
using Blog.Core.Config;
using Blog.Core.Module.DataService;
using Sixpence.Core.Logging;

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
        public mail_vertification GetDataByMailAdress(string mail, MailType mailType = MailType.Activation)
        {
            var sql = @"SELECT * FROM mail_vertification
WHERE mail_address = @address
AND is_active = 0
AND expire_time > CURRENT_TIMESTAMP
AND mail_type = @type";
            return Broker.Retrieve<mail_vertification>(sql, new Dictionary<string, object>() { { "@address", mail }, { "@type", mailType.ToString() } });
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
                    mailbox = model.code,
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

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ResetPassword(string id)
        {
            try
            {
                return Broker.ExecuteTransaction(() =>
                {
                    var data = GetData(id);
                    if (data == null)
                        return "重置失败";

                    if (data.expire_time < DateTime.Now)
                        return "重置失败，重置链接已过期";

                    new SystemService(Broker).ResetPassword(data.createdBy);
                    return $"重置成功，初始密码为：{SystemConfig.Config.DefaultPassword}";
                });
            }
            catch (Exception ex)
            {
                LogUtils.Error("重置密码失败", ex);
                return "服务器内部错误，请联系管理员";
            }
        }
    }
}
