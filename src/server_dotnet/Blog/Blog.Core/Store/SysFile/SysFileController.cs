using Blog.Core.Config;
using Blog.Core.Module.DataService;
using Blog.Core.Utils;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Core.Store.SysFile
{
    public class SysFileController : EntityBaseController<sys_file, SysFileService>
    {
        [HttpGet, AllowAnonymous]
        public Task<IActionResult> Download(string objectId)
        {
            var config = StoreConfig.Config;
            return ServiceContainer.Resolve<IStoreStrategy>(config?.Type).DownLoad(objectId);
        }

        [HttpPost]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public List<ImageInfo> Upload([FromForm]List<IFormFile> files, [FromQuery]string fileType, [FromQuery]string objectId = "")
        {
            if (files == null || !files.Any())
                throw new SpException("上传文件不能为空", "");

            var imgList = new List<ImageInfo>();

            foreach (var file in files)
            {
                var stream = file.OpenReadStream();

                // 获取文件哈希码，将哈希码作为文件名
                var hash_code = SHAUtil.GetFileSHA1(stream);
                var fileName = $"{hash_code}.{file.FileName.GetFileType()}";
                var config = StoreConfig.Config;
                ServiceContainer.Resolve<IStoreStrategy>(config?.Type).Upload(file.OpenReadStream(), fileName, out var filePath);
                var sysImage = new sys_file()
                {
                    sys_fileId = Guid.NewGuid().ToString(),
                    name = fileName,
                    hash_code = hash_code,
                    file_path = filePath,
                    file_type = fileType,
                    content_type = file.ContentType
                };
                if (!string.IsNullOrEmpty(objectId))
                {
                    sysImage.objectId = objectId;
                }
                new SysFileService().CreateData(sysImage);
                var imgInfo = new ImageInfo()
                {
                    id = sysImage.Id,
                    name = sysImage.name,
                    downloadUrl = $"api/SysFile/Download?objectId={sysImage.sys_fileId}"
                };
                imgList.Add(imgInfo);
            }

            return imgList;
        }
    }
}