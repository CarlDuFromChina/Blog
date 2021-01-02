#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/18 22:05:00
Description：同步微信素材Job
********************************************************/
#endregion

using SixpenceStudio.Core.UserInfo;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Logging;
using SixpenceStudio.WeChat;
using SixpenceStudio.WeChat.Material;
using System;

namespace SixpenceStudio.Blog.Jobs
{
    public class SyncMaterialJob : JobBase
    {
        public override string Name => "同步微信素材";

        public override string Description => "同步微信素材";

        public override string CronExperssion => "0 0 4 * * ?";

        public override void Execute(IPersistBroker broker)
        {
            var logger = LogFactory.GetLogger("wechat");
            logger.Debug("开始同步微信公众号素材");
            var user = broker.Retrieve<user_info>("5B4A52AF-052E-48F0-82BB-108CC834E864");
            try
            {
                var images = new WeChatMaterialService(broker).GetMaterial(MaterialType.image.ToMaterialTypeString(), 1, 5000);
                if (images.item_count == 0)
                {
                    logger.Debug($"未发现图片待同步");
                }
                else
                {
                    logger.Debug($"发现共{images.item_count}张图片待同步");
                    images.item.ForEach(item =>
                    {
                        var data = broker.Retrieve<wechat_material>(item.media_id);
                        if (data == null)
                        {
                            var material = new wechat_material()
                            {
                                wechat_materialId = item.media_id,
                                media_id = item.media_id,
                                url = item.url,
                                name = item.name,
                                type = MaterialType.image.ToMaterialTypeString(),
                                createdBy = user.Id,
                                createdByName = user.name,
                                modifiedBy = user.Id,
                                modifiedByName = user.name,
                                modifiedOn = DateTime.Now,
                                createdOn = DateTime.Now
                            };
                            broker.Create(material);
                        }
                        logger.Debug($"同步图片{item.name}成功");
                    });
                    logger.Debug($"微信图片素材同步成功，共同步{images.item_count}个");
                }


                var voices = new WeChatMaterialService(broker).GetMaterial(MaterialType.voice.ToMaterialTypeString(), 1, 5000);
                if (voices.item_count == 0)
                {
                    logger.Debug($"未发现语音待同步");
                }
                else
                {
                    logger.Debug($"发现共{images.item_count}个语音待同步");
                    voices.item.ForEach(item =>
                    {
                        var data = broker.Retrieve<wechat_material>(item.media_id);
                        if (data == null)
                        {
                            var material = new wechat_material()
                            {
                                wechat_materialId = item.media_id,
                                media_id = item.media_id,
                                url = item.url,
                                sys_fileid = "",
                                name = item.name,
                                type = MaterialType.voice.ToMaterialTypeString(),
                                createdBy = user.Id,
                                createdByName = user.name,
                                modifiedBy = user.Id,
                                modifiedByName = user.name,
                                modifiedOn = DateTime.Now,
                                createdOn = DateTime.Now
                            };
                            broker.Create(material);
                        }
                        logger.Debug($"同步语音{item.name}成功");
                    });
                    logger.Debug($"微信语音素材同步成功，共同步{images.item_count}个");
                }


                var videos = new WeChatMaterialService(broker).GetMaterial(MaterialType.video.ToMaterialTypeString(), 1, 5000);
                if (voices.item_count == 0)
                {
                    logger.Debug($"未发现视频待同步");
                }
                else
                {
                    logger.Debug($"发现共{images.item_count}个视频待同步");
                    videos.item.ForEach(item =>
                    {
                        var data = broker.Retrieve<wechat_material>(item.media_id);
                        if (data == null)
                        {
                            var material = new wechat_material()
                            {
                                wechat_materialId = item.media_id,
                                media_id = item.media_id,
                                url = item.url,
                                sys_fileid = "",
                                name = item.name,
                                type = MaterialType.video.ToMaterialTypeString(),
                                createdBy = user.Id,
                                createdByName = user.name,
                                modifiedBy = user.Id,
                                modifiedByName = user.name,
                                modifiedOn = DateTime.Now,
                                createdOn = DateTime.Now
                            };
                            broker.Create(material);
                            logger.Debug($"同步视频{item.name}成功");
                        }
                    });
                    logger.Debug($"微信视频素材同步成功，共同步{videos.item_count}个");
                }
            }
            catch (Exception ex)
            {
                logger.Debug($"微信素材同步失败");
                logger.Error($"{ex.Message}\r\n{ex.StackTrace}");
            }
        }
    }
}
