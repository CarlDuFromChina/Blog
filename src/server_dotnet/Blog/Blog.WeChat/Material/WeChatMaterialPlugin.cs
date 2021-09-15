#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/21 0:05:34
Description：素材Plugin
********************************************************/
#endregion

using Blog.Core;
using Blog.Core.Config;
using Blog.Core.Data;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using Sixpence.Core.Utils;
using Blog.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.Core;

namespace Blog.WeChat.Material
{
    public class WeChatMaterialPlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
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
                        var stream = StreamUtil.BytesToStream(result);
                        var hash_code = SHAUtil.GetFileSHA1(stream);
                        var config = StoreConfig.Config;
                        ServiceContainer.Resolve<IStoreStrategy>(config?.Type).Upload(stream, entity.name, out var filePath);
                        var sysImage = new sys_file()
                        {
                            sys_fileId = Guid.NewGuid().ToString(),
                            name = entity.name,
                            hash_code = hash_code,
                            file_path = filePath,
                            file_type = "wechat_material",
                            content_type = contentType,
                            objectId = entity.Id
                        };
                        var id = new SysFileService(context.Broker).CreateData(sysImage);
                        entity.sys_fileid = id;
                        entity.local_url = $"/api/SysFile/Download?objectId={id}";
                    }
                    break;
                case EntityAction.PreDelete:
                    WeChatApi.DeleteMaterial(entity.GetAttributeValue<string>("media_id"));
                    break;
                default:
                    break;
            }
        }
    }
}
