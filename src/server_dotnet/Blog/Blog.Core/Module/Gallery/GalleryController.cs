using Blog.Core.Module.DataService;
using Blog.Core.Pixabay;
using Blog.Core.Profiles;
using Blog.Core.Store.SysFile;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sixpence.Core;
using Sixpence.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Module.Gallery
{
    public class GalleryController : EntityBaseController<gallery, GalleryService>
    {
        [HttpGet]
        public ImagesModel GetImages(string searchValue, int pageIndex, int pageSize)
        {
            return new PixabayService().GetImages(searchValue, pageIndex, pageSize);
        }

        [HttpPost]
        public List<string> UploadImage(ImageModel image)
        {
            return new GalleryService().UploadImage(image);
        }

        /// <summary>
        /// 上传图片，自动生成预览图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<ImageInfo> UploadImage([FromForm] IFormFile file, [FromQuery] string fileType, [FromQuery] string objectId)
        {
            var stream = file.OpenReadStream();
            var contentType = file.ContentType;
            var suffix = file.FileName.GetFileType();

            var image = new SysFileService().UploadFile(stream, suffix, fileType, contentType, objectId);

            var thumbStream = ImageUtil.GetThumbnail(Path.Combine(FolderType.Storage.GetPath(), image?.name ?? ""));
            var image2 = new SysFileService().UploadFile(thumbStream, suffix, fileType, contentType, objectId);

            return new List<ImageInfo>()
            {
                MapperHelper.Map<ImageInfo>(image),
                MapperHelper.Map<ImageInfo>(image2)
            };
        }
    }
}
