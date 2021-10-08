#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/17 22:37:09
Description：同步微信图文素材Job
********************************************************/
#endregion

using Newtonsoft.Json;
using System;
using System.Linq;
using Quartz;
using Blog.Core.Job;
using Sixpence.EntityFramework.Entity;
using Blog.WeChat.WeChatNews;
using Blog.Core.Auth.UserInfo;
using Sixpence.EntityFramework.Broker;
using Blog.Core.Auth;
using Blog.WeChat.WeChatNewsMaterial;

namespace Blog.WeChat.Job
{
    public class SyncNewsMaterialJob : JobBase
    {
        public override string Name => "同步微信图文素材";

        public override string Description => "同步微信图文素材";

        public override IScheduleBuilder ScheduleBuilder => CronScheduleBuilder.CronSchedule("0 0 4 * * ?");

        public override TriggerState DefaultTriggerState => TriggerState.Paused;

        public override void Executing(IJobExecutionContext context)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            Logger.Debug("开始同步微信公众号图文素材");
            try
            {
                broker.ExecuteTransaction(() =>
                {
                    var result = new WeChatNewsService(broker).GetDataList(1, 5000);
                    var user = UserIdentityUtil.GetCurrentUser() ?? UserIdentityUtil.GetSystem();
                    var dataList = (from item in result.item
                                    let news = item.content.news_item.FirstOrDefault()
                                    select new wechat_news()
                                    {
                                        wechat_newsId = item.media_id,
                                        html_content = new WeChatNewsMaterialService(broker).ConvertWeChatUrlToLocalUrl(news?.content),
                                        media_id = item.media_id,
                                        update_time = item.update_time,
                                        name = news?.title,
                                        author = news?.author,
                                        digest = news?.digest,
                                        thumb_media_id = news?.thumb_media_id,
                                        content_source_url = news?.content_source_url,
                                        createdBy = user.Id,
                                        createdByName = user.Name,
                                        modifiedBy = user.Id,
                                        modifiedByName = user.Name,
                                        createdOn = DateTime.Now,
                                        modifiedOn = DateTime.Now,
                                    })
                                   .ToList();
                    broker.BulkCreateOrUpdate(dataList);
                    Logger.Debug($"发现{result.item_count}篇文章需要同步");
                    Logger.Debug($"同步微信公众号图文素材成功，共同步：{dataList.Count()}篇文章");
                });
            }
            catch (Exception e)
            {
                Logger.Error("同步微信公众号图文素材失败", e);
            }
        }
    }
}
