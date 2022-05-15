#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/17 22:37:09
Description：同步微信图文素材Job
********************************************************/
#endregion

using Blog.Core.Auth;
using Blog.Core.Job;
using Blog.WeChat.WeChatNews;
using Blog.WeChat.WeChatNewsMaterial;
using Quartz;
using Sixpence.ORM.EntityManager;
using System;
using System.Linq;

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
            var Manager = EntityManagerFactory.GetManager();
            Logger.Debug("开始同步微信公众号图文素材");
            try
            {
                Manager.ExecuteTransaction(() =>
                {
                    var result = new WeChatNewsService(Manager).GetDataList(1, 5000);
                    var user = UserIdentityUtil.GetCurrentUser() ?? UserIdentityUtil.GetSystem();
                    var dataList = (from item in result.item
                                    let news = item.content.news_item.FirstOrDefault()
                                    select new wechat_news()
                                    {
                                        id = item.media_id,
                                        html_content = new WeChatNewsMaterialService(Manager).ConvertWeChatUrlToLocalUrl(news?.content),
                                        media_id = item.media_id,
                                        update_time = item.update_time,
                                        name = news?.title,
                                        author = news?.author,
                                        digest = news?.digest,
                                        thumb_media_id = news?.thumb_media_id,
                                        content_source_url = news?.content_source_url,
                                        created_by = user.Id,
                                        created_by_name = user.Name,
                                        updated_by = user.Id,
                                        updated_by_name = user.Name,
                                        updated_at = DateTime.Now,
                                        created_at = DateTime.Now
                                    })
                                   .ToList();
                    Manager.BulkCreateOrUpdate(dataList);
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
