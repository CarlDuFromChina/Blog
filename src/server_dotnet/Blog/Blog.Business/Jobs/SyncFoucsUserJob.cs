#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/17 22:36:22
Description：同步微信关注用户Job
********************************************************/
#endregion

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Blog.Core.Job;
using Blog.WeChat.FocusUser;
using Sixpence.EntityFramework.Entity;
using Blog.Core.Auth;
using Sixpence.Core.Utils;
using Sixpence.Core;
using Sixpence.EntityFramework.Broker;

namespace Blog.Jobs
{
    public class SyncFoucsUserJob : JobBase
    {
        public override string Name => "同步微信关注用户";

        public override string Description => "同步微信关注用户（手动）";

        public override TriggerState DefaultTriggerState => TriggerState.Paused;

        public override void Executing(IJobExecutionContext context)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var focusUserService = new FocusUserService();
            var user = UserIdentityUtil.GetCurrentUser();
            var focusUserList = focusUserService.GetFocusUserList();
            Logger.Debug($"获取到{focusUserList.count}条微信公众号关注用户");
            if (focusUserList.count > 0)
            {
                var user_list = new List<OpenId>();
                focusUserList.data.openid.ForEach(item => user_list.Add(new OpenId() { openid = item, lang = "zh_CN" }));
                var focusUsers = focusUserService.GetFocusUsers(JsonConvert.SerializeObject(new { user_list }));
                broker.ExecuteTransaction(() =>
                {
                    var dataList = focusUsers.user_list
                        .Select(focusUser =>
                        {
                                  var wechat_user = new wechat_user()
                                  {
                                      Id = focusUser.openid,
                                      subscribe = Convert.ToInt32(focusUser.subscribe),
                                      nickname = focusUser.nickname,
                                      name = focusUser.nickname,
                                      sex = focusUser.sex,
                                      language = focusUser.language,
                                      city = focusUser.city,
                                      province = focusUser.province,
                                      country = focusUser.country,
                                      headimgurl = focusUser.headimgurl,
                                      subscribe_time = focusUser.subscribe_time.ToDateTime(),
                                      unionid = focusUser.unionid,
                                      remark = focusUser.remark,
                                      groupid = focusUser.groupid,
                                      subscribe_scene = focusUser.subscribe_scene,
                                      qr_scene = focusUser.qr_scene,
                                      qr_scene_str = focusUser.qr_scene_str,
                                      createdBy = user.Id,
                                      createdByName = user.Name,
                                      modifiedBy = user.Id,
                                      modifiedByName = user.Name,
                                      createdOn = DateTime.Now,
                                      modifiedOn = DateTime.Now,
                                  };
                                  return wechat_user;
                              })
                        .ToList();
                    broker.BulkCreateOrUpdate(dataList);
                    Logger.Debug($"创建或更新{dataList.Count}条关注用户信息");
                });
            }
        }
    }
}
