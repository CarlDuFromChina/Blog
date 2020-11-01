using Minio.DataModel;
using Newtonsoft.Json;
using SixpenceStudio.BaseSite.UserInfo;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Job;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.WeChat;
using SixpenceStudio.WeChat.WeChatNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.WeChat.Job
{
    public class WeChatSyncJob : JobBase
    {
        public override string Name => "同步微信公众号数据";

        public override string Description => "同步微信公众号数据（微信图文素材、视频、语音、图片）";

        public override string CronExperssion => "";

        public override void Run(IPersistBroker broker)
        {
            SyncWeChatNews(broker);
        }

        private void SyncWeChatNews(IPersistBroker broker)
        {
            LogUtils.Debug("开始同步微信公众号图文素材");
            try
            {
                var result = WeChatService.GetWeChatNewsMaterial("news", 1, 5000);
                var user = broker.Retrieve<user_info>("5B4A52AF-052E-48F0-82BB-108CC834E864");
                var dataList = from item in result.item
                               select new wechat_news()
                               {
                                   wechat_newsId = item.media_id,
                                   content = JsonConvert.SerializeObject(item.content),
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
                LogUtils.Debug("同步微信公众号图文素材成功");
            }
            catch (Exception e)
            {
                LogUtils.Error("同步微信公众号图文素材失败", e);
            }
        }
    }
}
