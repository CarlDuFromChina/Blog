#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/17 22:37:09
Description：同步微信图文素材Job
********************************************************/
#endregion

using Newtonsoft.Json;
using SixpenceStudio.BaseSite.UserInfo;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Job;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.WeChat.WeChatNews;
using System;
using System.Linq;

namespace SixpenceStudio.Blog.WeChat.SyncJobs
{
    public class SyncNewsMaterialJob : JobBase
    {
        public override string Name => "同步微信图文素材";

        public override string Description => "同步微信图文素材（手动）";

        public override string CronExperssion => "";

        public override void Execute(IPersistBroker broker)
        {
            var logger = LogFactory.GetLogger("wechat");
            logger.Debug("开始同步微信公众号图文素材");
            try
            {
                var result = new WeChatNewsService().GetNewsMaterial(1, 5000);
                var user = broker.Retrieve<user_info>("5B4A52AF-052E-48F0-82BB-108CC834E864");
                var dataList = from item in result.item
                               select new wechat_news()
                               {
                                   wechat_newsId = item.media_id,
                                   content = JsonConvert.SerializeObject(item.content.news_item.FirstOrDefault()?.content),
                                   media_id = item.media_id,
                                   update_time = item.update_time,
                                   name = item.content.news_item.FirstOrDefault()?.title,
                                   author = item.content.news_item.FirstOrDefault()?.author,
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
                dataList.ToList().ForEach(item =>
                {
                    broker.Save(item);
                });
                logger.Debug("同步微信公众号图文素材成功");
            }
            catch (Exception e)
            {
                logger.Error("同步微信公众号图文素材失败", e);
            }
        }
    }
}
