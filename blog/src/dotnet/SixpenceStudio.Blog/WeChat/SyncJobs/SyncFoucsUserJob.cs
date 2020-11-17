#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/17 22:36:22
Description：同步微信关注用户Job
********************************************************/
#endregion

using Newtonsoft.Json;
using SixpenceStudio.BaseSite.UserInfo;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Job;
using SixpenceStudio.WeChat.FocusUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.WeChat.SyncJobs
{
    public class SyncFoucsUserJob : JobBase
    {
        public override string Name => "同步微信关注用户";

        public override string Description => "同步微信关注用户（手动）";

        public override string CronExperssion => "";

        public override void Execute(IPersistBroker broker)
        {
            var focusUserService = new FocusUserService();
            var focusUserList = focusUserService.GetFocusUserList();

            var user = broker.Retrieve<user_info>("5B4A52AF-052E-48F0-82BB-108CC834E864");
            if (focusUserList.count > 0)
            {
                var user_list = new List<OpenId>();
                focusUserList.data.openid.ForEach(item => user_list.Add(new OpenId() { openid = item, lang = "zh_CN" }));
                var focusUsers = focusUserService.GetFocusUsers(JsonConvert.SerializeObject(new { user_list }));
                var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                broker.ExecuteTransaction(() =>
                {
                    focusUsers.user_list.ForEach(focusUser =>
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
                            subscribe_time = start.AddMilliseconds(focusUser.subscribe_time * 1000).ToLocalTime(),
                            unionid = focusUser.unionid,
                            remark = focusUser.remark,
                            groupid = focusUser.groupid,
                            subscribe_scene = focusUser.subscribe_scene,
                            qr_scene = focusUser.qr_scene,
                            qr_scene_str = focusUser.qr_scene_str,
                            createdBy = user.user_infoId,
                            createdByName = user.name,
                            modifiedBy = user.user_infoId,
                            modifiedByName = user.name,
                            createdOn = DateTime.Now,
                            modifiedOn = DateTime.Now,
                        };
                        broker.Save(wechat_user);
                    });
                });
            }
        }
    }
}
