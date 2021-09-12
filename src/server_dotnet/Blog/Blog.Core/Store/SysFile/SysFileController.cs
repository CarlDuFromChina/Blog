using Blog.Core.Config;
using Blog.Core.Module.DataService;
using Blog.Core.Profiles;
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
        public List<FileInfoModel> Upload([FromForm]List<IFormFile> files, [FromQuery]string fileType, [FromQuery]string objectId = "")
        {
            if (files == null || !files.Any())
                throw new SpException("上传文件不能为空", "");

            var fileList = new List<FileInfoModel>();

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
                var fileModel = new FileInfoModel
                {
                    id = sysImage.Id,
                    name = sysImage.name,
                    downloadUrl = $"api/SysFile/Download?objectId={sysImage.sys_fileId}"
                };
                fileList.Add(fileModel);
            }

            return fileList;
        }

        /// <summary>
        /// 上传图片，自动生成预览图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<ImageInfo> UploadImage([FromForm]IFormFile file, [FromQuery]string fileType, [FromQuery]string objectId)
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