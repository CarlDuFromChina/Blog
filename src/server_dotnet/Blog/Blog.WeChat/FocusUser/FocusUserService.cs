using Newtonsoft.Json;
using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Sixpence.Core.Logging;

namespace Blog.WeChat.FocusUser
{
    public class FocusUserService
    {
        IPersistBroker broker;
        ILog logger;

        public FocusUserService()
        {
            broker = PersistBrokerFactory.GetPersistBroker();
            logger = LogFactory.GetLogger("wechat");
        }

        /// <summary>
        /// 获取所有关注用户（返回OpenId集合）
        /// </summary>
        /// <returns></returns>
        public FocusUserListModel GetFocusUserList()
        {
            var resp = WeChatApi.GetFocusUserList();
            var openIds = JsonConvert.DeserializeObject<FocusUserListModel>(resp);
            return openIds;
        }

        /// <summary>
        /// 获取关注用户信息
        /// </summary>
        /// <param name="userList">OpenId列表</param>
        /// <returns></returns>
        public FocusUsersModel GetFocusUsers(string userList)
        {
            var resp2 = WeChatApi.BatchGetFocusUser(JsonConvert.SerializeObject(userList));
            var focusUsers = JsonConvert.DeserializeObject<FocusUsersModel>(resp2);
            return focusUsers;
        }
    }
}
