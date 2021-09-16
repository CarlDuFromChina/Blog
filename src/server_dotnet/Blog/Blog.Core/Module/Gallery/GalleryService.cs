using Blog.Core.Config;
using Sixpence.EntityFramework.Entity;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using Sixpence.Core;
using Sixpence.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.EntityFramework.Broker;

namespace Blog.Core.Module.Gallery
{
    public class GalleryService : EntityService<gallery>
    {
        #region 构造函数
        public GalleryService()
        {
            _context = new EntityContext<gallery>();
        }

        public GalleryService(IPersistBroker broker)
        {
            _context = new EntityContext<gallery>(Broker);
        }
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
                    OrderBy = "createdon desc"
                }
            };
        }

        private string DownloadImage(string url, string objectid)
        {
            var result = HttpUtil.DownloadImage(url, out var contentType);
            var stream = StreamUtil.BytesToStream(result);
            var hash_code = SHAUtil.GetFileSHA1(stream);

            var config = StoreConfig.Config;
            var fileName = $"{hash_code}.{url.Substring(url.LastIndexOf("/") + 1).GetFileType()}";
            ServiceContainer.Resolve<IStoreStrategy>(config?.Type).Upload(stream, fileName, out var filePath);

            var data = new sys_file()
            {
                sys_fileId = Guid.NewGuid().ToString(),
                name = fileName,
                hash_code = hash_code,
                file_path = filePath,
                file_type = "gallery",
                content_type = contentType,
                objectId = objectid
            };
            return Broker.Create(data);
        }

        public List<string> UploadImage(Pixabay.ImageModel image)
        {
            return Broker.ExecuteTransaction(() =>
            {
                var data = new gallery()
                {
                    Id = Guid.NewGuid().ToString(),
                    tags = image.tags
                };
                data.previewid = DownloadImage(image.previewURL, data.Id);
                data.imageid = DownloadImage(image.largeImageURL, data.Id);
                data.preview_url = $"api/SysFile/Download?objectid={data.previewid}";
                data.image_url = $"api/SysFile/Download?objectid={data.imageid}";
                base.CreateData(data);
                return new List<string>() { data.previewid, data.imageid };
            });
        }
    }
}
