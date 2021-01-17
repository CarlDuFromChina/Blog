#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/17 22:37:09
Description：同步微信图文素材Job
********************************************************/
#endregion

using Newtonsoft.Json;
using SixpenceStudio.Core.UserInfo;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Utils;
using SixpenceStudio.WeChat.WeChatNews;
using System;
using System.Linq;
using Quartz;

namespace SixpenceStudio.Blog.Jobs
{
    public class SyncNewsMaterialJob : JobBase
    {
        public override string Name => "同步微信图文素材";

        public override string Description => "同步微信图文素材";

        public override IScheduleBuilder ScheduleBuilder => CronScheduleBuilder.CronSchedule("0 0 4 * * ?");

        public override void Executing(IJobExecutionContext context)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            Logger.Debug("开始同步微信公众号图文素材");
            try
            {
                var result = new WeChatNewsService(broker).GetDataList(1, 5000);
                var user = broker.Retrieve<user_info>("5B4A52AF-052E-48F0-82BB-108CC834E864");
                var dataList = from item in result.item
                               let news = item.content.news_item.FirstOrDefault()
                               select new wechat_news()
                               {
                                   wechat_newsId = item.media_id,
                                   content = JsonConvert.SerializeObject(news?.content),
                                   media_id = item.media_id,
                                   update_time = item.update_time,
                                   name = news?.title,
                                   author = news?.author,
                                   digest = news?.digest,
                                   thumb_media_id = news?.thumb_media_id,
                                   createdBy = user.user_infoId,
                                   createdByName = user.name,
                                   modifiedBy = user.user_infoId,
                                   modifiedByName = user.name,
                                   createdOn = DateTime.Now,
                                   modifiedOn = DateTime.Now,
                               };

                if (dataList == null || dataList.Count() == 0)
                {
                    return;
                }

                Logger.Debug($"发现{result.item_count}篇文章需要同步");
                dataList.Each(item =>
                {
                    broker.Save(item);
                    Logger.Debug($"同步文章：{item.name}成功");
                });
                Logger.Debug($"同步微信公众号图文素材成功，共同步：{dataList.Count()}篇文章");
            }
            catch (Exception e)
            {
                Logger.Error("同步微信公众号图文素材失败", e);
            }
        }
    }
}
