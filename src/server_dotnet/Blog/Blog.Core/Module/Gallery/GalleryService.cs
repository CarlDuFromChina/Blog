using Blog.Core.Config;
using Sixpence.ORM.Entity;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using Sixpence.Common;
using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.Common.IoC;
using Sixpence.ORM.Repository;
using Sixpence.ORM.EntityManager;

namespace Blog.Core.Module.Gallery
{
    public class GalleryService : EntityService<gallery>
    {
        #region 构造函数
        public GalleryService() : base() { }

        public GalleryService(IEntityManager manager) : base(manager) { }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            return new List<EntityView>() {
                new EntityView()
                {
                    ViewId = "0F0DC786-CF7D-4997-B42C-47FB09B12AAE",
                    Sql = "select * from gallery where 1 = 1",
                    CustomFilter = new List<string>(){ "tags" },
                    Name = "本地图库",
                    OrderBy = "created_at desc"
                }
            };
        }

        private string DownloadImage(string url, string objectid)
        {
            var result = HttpUtil.DownloadImage(url, out var contentType);
            var stream = StreamUtil.BytesToStream(result);
            var hash_code = SHAUtil.GetFileSHA1(stream);

            var config = StoreConfig.Config;
            var id = Guid.NewGuid().ToString();
            var fileName = $"{id}.{url.Substring(url.LastIndexOf("/") + 1).GetFileType()}";
            ServiceContainer.Resolve<IStoreStrategy>(config?.Type).Upload(stream, fileName, out var filePath);

            var data = new sys_file()
            {
                id = id,
                name = fileName,
                real_name = fileName,
                hash_code = hash_code,
                file_type = "gallery",
                content_type = contentType,
                objectId = objectid
            };
            return Manager.Create(data);
        }

        public List<string> UploadImage(Pixabay.ImageModel image)
        {
            return Manager.ExecuteTransaction(() =>
            {
                var data = new gallery()
                {
                    id = Guid.NewGuid().ToString(),
                    tags = image.tags
                };
                data.previewid = DownloadImage(image.previewURL, data.id);
                data.imageid = DownloadImage(image.largeImageURL, data.id);
                data.preview_url = SysFileService.GetDownloadUrl(data.previewid);
                data.image_url = SysFileService.GetDownloadUrl(data.imageid);
                base.CreateData(data);
                return new List<string>() { data.previewid, data.imageid };
            });
        }
    }
}
