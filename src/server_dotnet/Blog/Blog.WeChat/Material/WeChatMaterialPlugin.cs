#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/21 0:05:34
Description：素材Plugin
********************************************************/
#endregion

using Blog.Core.Config;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using Sixpence.Common.IoC;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using System;

namespace Blog.WeChat.Material
{
    public class WeChatMaterialPlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            var entity = context.Entity as wechat_material;
            switch (context.Action)
            {
                case EntityAction.PreCreate:
                case EntityAction.PreUpdate:
                    // 如果素材未上传到系统，则根据url请求图片保存
                    if (string.IsNullOrEmpty(entity.sys_fileid))
                    {
                        var result = HttpUtil.DownloadImage(entity.url, out var contentType);
                        var id = Guid.NewGuid().ToString();
                        var stream = StreamUtil.BytesToStream(result);
                        var hash_code = SHAUtil.GetFileSHA1(stream);
                        var config = StoreConfig.Config;
                        ServiceContainer.Resolve<IStoreStrategy>(config?.Type).Upload(stream, entity.name, out var filePath);
                        var sysImage = new sys_file()
                        {
                            id = id,
                            name = entity.name,
                            real_name = entity.name,
                            hash_code = hash_code,
                            file_type = "wechat_material",
                            content_type = contentType,
                            objectId = entity.id
                        };
                        new SysFileService(context.EntityManager).CreateData(sysImage);
                        entity.sys_fileid = id;
                        entity.local_url = SysFileService.GetDownloadUrl(id);
                    }
                    break;
                case EntityAction.PreDelete:
                    WeChatApi.DeleteMaterial(entity.GetAttributeValue<string>("media_id"));
                    context.EntityManager.Delete("sys_file", entity.GetAttributeValue<string>("sys_fileid"));
                    break;
                default:
                    break;
            }
        }
    }
}
