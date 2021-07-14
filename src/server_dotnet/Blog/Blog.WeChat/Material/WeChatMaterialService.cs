using Newtonsoft.Json;
using Blog.Core.WebApi;
using Blog.Core.Data;
using System;
using System.Collections.Generic;
using Blog.Core;
using Blog.Core.Store.SysFile;
using Blog.Core.Utils;
using Blog.Core.Config;
using Blog.Core.Store;
using Blog.Core.Auth;

namespace Blog.WeChat.Material
{
    public class WeChatMaterialService : EntityService<wechat_material>
    {
        #region 构造函数
        public WeChatMaterialService()
        {
            _context = new EntityContext<wechat_material>();
        }

        public WeChatMaterialService(IPersistBroker broker)
        {
            _context = new EntityContext<wechat_material>(broker);
        }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            var sql = $"SELECT * FROM wechat_material WHERE 1=1";
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = sql,
                    CustomFilter = new List<string>() { "name" },
                    OrderBy = "createdon, name",
                    ViewId = ""
                }
            };
        }

        /// <summary>
        /// 获取素材
        /// </summary>
        /// <param name="type">素材的类型，图片（image）、视频（video）、语音 （voice）、图文（news）</param>
        /// <param name="pageIndex">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="pageSize">返回素材的数量，取值在1到20之间</param>
        /// <returns></returns>
        public WeChatOtherMaterial GetMaterial(string type, int pageIndex, int pageSize)
        {
            var result = WeChatApi.BatchGetMaterial(type, pageIndex, pageSize);
            var materialList = JsonConvert.DeserializeObject<WeChatOtherMaterial>(result);
            if (materialList == null || materialList.item == null || materialList.item.Count <= 0)
            {
                return materialList;
            }

            materialList.item.Each(item =>
            {
                item.UpdateTime = item.update_time.ToDateTimeString();
            });
            return materialList;
        }

        /// <summary>
        /// 上传素材
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public string CreateData(MaterialType type, string fileId)
        {
            var file = new SysFileService().GetData(fileId);
            AssertUtil.CheckBoolean<SpException>(file == null, $"根据fileid：{fileId}未找到记录", "36B5F5C9-ED65-4CAC-BE60-712278056EA9");

            // 检查素材库是否已经上传
            var data = Broker.Retrieve<wechat_material>("select * from wechat_material where sys_fileid = @sys_fileid", new Dictionary<string, object>() { { "@sys_fileid", file.sys_fileId } });
            if (data != null)
            {
                return data.media_id;
            }

            // 获取文件流
            var config = StoreConfig.Config;
            var stream = ServiceContainer.Resolve<IStoreStrategy>(config?.Type).GetStream(fileId);
            var media = WeChatApi.AddMaterial(type, stream, file.name, file.content_type);
            
            // 创建素材记录
            var user = UserIdentityUtil.GetCurrentUser();
            var material = new wechat_material()
            {
                wechat_materialId = Guid.NewGuid().ToString(),
                media_id = media.media_id,
                url = media.url,
                sys_fileid = fileId,
                name = file.name,
                type = type.ToMaterialTypeString(),
                createdBy = user.Id,
                createdByName = user.Name,
                modifiedBy = user.Id,
                modifiedByName= user.Name,
                modifiedOn = DateTime.Now,
                createdOn = DateTime.Now
            };
            CreateData(material);
            return media.media_id;
        }
    }
}
