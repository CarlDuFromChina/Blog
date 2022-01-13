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
using Sixpence.ORM.Entity;
using Blog.Core.Auth;
using Sixpence.Common.Utils;
using Sixpence.Common;
using Sixpence.ORM.EntityManager;

namespace Blog.Jobs
{
    public class SyncFoucsUserJob : JobBase
    {
        public override string Name => "同步微信关注用户";

        public override string Description => "同步微信关注用户（手动）";

        public override TriggerState DefaultTriggerState => TriggerState.Paused;

        public override void Executing(IJobExecutionContext context)
        {
            var manager = EntityManagerFactory.GetManager();
            var focusUserService = new FocusUserService();
            var user = UserIdentityUtil.GetCurrentUser();
            var focusUserList = focusUserService.GetFocusUserList();
            Logger.Debug($"获取到{focusUserList.count}条微信公众号关注用户");
            if (focusUserList.count > 0)
            {
                var user_list = new List<OpenId>();
                focusUserList.data.openid.ForEach(item => user_list.Add(new OpenId() { openid = item, lang = "zh_CN" }));
                var focusUsers = focusUserService.GetFocusUsers(JsonConvert.SerializeObject(new { user_list }));
                manager.ExecuteTransaction(() =>
                {
                    var dataList = focusUsers.user_list
                        .Select(focusUser =>
                        {
                                  var wechat_user = new wechat_user()
                                  {
                                      id = focusUser.openid,
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
                                      created_by = user.Id,
                                      created_by_name = user.Name,
                                      updated_by = user.Id,
                                      updated_by_name = user.Name,
                                      created_at = DateTime.Now,
                                      updated_at = DateTime.Now,
                                  };
                                  return wechat_user;
                              })
                        .ToList();
                    manager.BulkCreateOrUpdate(dataList);
                    Logger.Debug($"创建或更新{dataList.Count}条关注用户信息");
                });
            }
        }
    }
}
