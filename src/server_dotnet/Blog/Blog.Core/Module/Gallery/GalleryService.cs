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
using Blog.Core.Module.Gallery.Model;
using Newtonsoft.Json;
using Blog.Core.Profiles;

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

        /// <summary>
        /// 下载指定Url图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="objectid"></param>
        /// <returns></returns>
        private string DownloadImage(string url, string objectid, string source = "gallery")
        {
            var result = HttpUtil.DownloadImage(url, out var contentType);
            var stream = StreamUtil.BytesToStream(result);
            var hash_code = SHAUtil.GetFileSHA1(stream);

            var config = StoreConfig.Config;
            var id = Guid.NewGuid().ToString();
            var originFileName = url.Substring(url.LastIndexOf("/") + 1); // 原始图片名
            var fileType = originFileName.GetFileType(); // 文件类型
            var fileName = $"{id}.{fileType}"; // 新文件名
            ServiceContainer.Resolve<IStoreStrategy>(config?.Type).Upload(stream, fileName, out var filePath);

            var data = new sys_file()
            {
                id = id,
                name = fileName,
                real_name = fileName,
                hash_code = hash_code,
                file_type = source,
                content_type = contentType,
                objectId = objectid
            };
            return Manager.Create(data);
        }

        /// <summary>
        /// 上传图片（Pixabay）
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取随机图片
        /// </summary>
        public gallery GetRandomImage()
        {
            var result = HttpUtil.Get("https://api.ixiaowai.cn/api/api.php?return=json");
            var model = JsonConvert.DeserializeObject<RandomImageModel>(result);
            var originFileName = model.imgurl.Substring(model.imgurl.LastIndexOf("/") + 1); // 原始图片名
            var imgBytes = HttpUtil.DownloadImage(model.imgurl, out var contentType);
            var imgStream = StreamUtil.BytesToStream(imgBytes);
            var suffix = model.imgurl.GetFileType();
            var sysFileService = new SysFileService(Manager);

            return Manager.ExecuteTransaction(() =>
            {
                var galleryid = Guid.NewGuid().ToString();
                var image = sysFileService.UploadFile(imgStream, suffix, "gallery", contentType, galleryid, originFileName);
                var thumbStream = ImageUtil.GetThumbnail(image.GetFilePath());
                var image2 = sysFileService.UploadFile(thumbStream, suffix, "gallery", contentType, galleryid, sysFileService.GetPreviewImageFileName(originFileName));

                var gallery = new gallery()
                {
                    id = galleryid,
                    previewid = image2.id,
                    preview_url = image2.DownloadUrl,
                    imageid = image.id,
                    image_url = image.DownloadUrl
                };
                Manager.Create(gallery);

                return gallery;
            });
        }
    }
}
