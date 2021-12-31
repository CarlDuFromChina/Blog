using Blog.Core.Module.DataService;
using Blog.Core.Pixabay;
using Blog.Core.Profiles;
using Blog.Core.Store.SysFile;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sixpence.Common;
using Sixpence.Common.Utils;
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
    }
}
