using Blog.Core.Pixabay;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blog.Core.Module.Gallery
{
    public class GalleryController : EntityBaseController<gallery, GalleryService>
    {
        /// <summary>
        /// 搜索云图库
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("image")]
        public ImagesModel GetImages(string searchValue, int pageIndex, int pageSize)
        {
            return new PixabayService().GetImages(searchValue, pageIndex, pageSize);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("image")]
        public List<string> UploadImage(ImageModel image)
        {
            return new GalleryService().UploadImage(image);
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RandomImage")]
        public gallery RandomImage()
        {
            return new GalleryService().GetRandomImage();
        }
    }
}
