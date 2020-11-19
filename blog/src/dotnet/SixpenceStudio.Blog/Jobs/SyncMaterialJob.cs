#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/18 22:05:00
Description：同步微信素材Job
********************************************************/
#endregion

using SixpenceStudio.BaseSite.UserInfo;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Job;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.WeChat;
using SixpenceStudio.WeChat.Material;
using System;

namespace SixpenceStudio.Blog.Jobs
{
    public class SyncMaterialJob : JobBase
    {
        public override string Name => "同步微信素材";

        public override string Description => "同步微信素材（手动）";

        public override string CronExperssion => "";

        public override void Execute(IPersistBroker broker)
        {
            var logger = LogFactory.GetLogger("wechat");
            logger.Debug("开始同步微信公众号素材");
            var user = broker.Retrieve<user_info>("5B4A52AF-052E-48F0-82BB-108CC834E864");
            try
            {
                var images = new WeChatMaterialService().GetMaterial(WeChatMaterialExtension.MaterialType.image.ToMaterialTypeString(), 1, 5000);
                images.item.ForEach(item =>
                {
                    var material = new wechat_material()
                    {
                        wechat_materialId = item.media_id,
                        media_id = item.media_id,
                        url = item.url,
                        sys_fileid = "",
                        name = item.name,
                        type = WeChatMaterialExtension.MaterialType.image.ToMaterialTypeString(),
                        createdBy = user.Id,
                        createdByName = user.name,
                        modifiedBy = user.Id,
                        modifiedByName = user.name,
                        modifiedOn = DateTime.Now,
                        createdOn = DateTime.Now
                    };
                    broker.Save(material);
                    logger.Debug($"同步图片{item.name}成功");
                });
                logger.Debug("微信图片素材同步成功");

                var voices = new WeChatMaterialService().GetMaterial(WeChatMaterialExtension.MaterialType.voice.ToMaterialTypeString(), 1, 5000);
                voices.item.ForEach(item =>
                {
                    var material = new wechat_material()
                    {
                        wechat_materialId = item.media_id,
                        media_id = item.media_id,
                        url = item.url,
                        sys_fileid = "",
                        name = item.name,
                        type = WeChatMaterialExtension.MaterialType.voice.ToMaterialTypeString(),
                        createdBy = user.Id,
                        createdByName = user.name,
                        modifiedBy = user.Id,
                        modifiedByName = user.name,
                        modifiedOn = DateTime.Now,
                        createdOn = DateTime.Now
                    };
                    broker.Save(material);
                    logger.Debug($"同步语音{item.name}成功");
                });
                logger.Debug("微信语音素材同步成功");

                var videos = new WeChatMaterialService().GetMaterial(WeChatMaterialExtension.MaterialType.voice.ToMaterialTypeString(), 1, 5000);
                videos.item.ForEach(item =>
                {
                    var material = new wechat_material()
                    {
                        wechat_materialId = item.media_id,
                        media_id = item.media_id,
                        url = item.url,
                        sys_fileid = "",
                        name = item.name,
                        type = WeChatMaterialExtension.MaterialType.video.ToMaterialTypeString(),
                        createdBy = user.Id,
                        createdByName = user.name,
                        modifiedBy = user.Id,
                        modifiedByName = user.name,
                        modifiedOn = DateTime.Now,
                        createdOn = DateTime.Now
                    };
                    logger.Debug($"同步视频{item.name}成功");
                    broker.Save(material);
                });
                logger.Debug("微信视频素材同步成功");
            }
            catch (Exception ex)
            {
                logger.Error("微信素材同步失败：\r\n" + ex.Message);
            }
        }
    }
}
