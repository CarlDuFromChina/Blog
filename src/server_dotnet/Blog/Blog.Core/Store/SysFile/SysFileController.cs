using Blog.Core.Config;
using Blog.Core.Module.DataService;
using Blog.Core.Profiles;
using Sixpence.Core.Utils;
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
using Sixpence.Core;

namespace Blog.Core.Store.SysFile
{
    public class SysFileController : EntityBaseController<sys_file, SysFileService>
    {
        /// <summary>
        /// 通用下载接口
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public Task<IActionResult> Download(string objectId)
        {
            var config = StoreConfig.Config;
            return ServiceContainer.Resolve<IStoreStrategy>(config?.Type).DownLoad(objectId);
        }

        /// <summary>
        /// 通用上传接口
        /// </summary>
        /// <param name="files"></param>
        /// <param name="fileType"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public List<FileInfoModel> Upload([FromForm] List<IFormFile> files, [FromQuery] string fileType, [FromQuery] string objectId = "")
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

        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public ImageInfo UploadImage([FromForm] IFormFile file, [FromQuery]string fileType, [FromQuery]string objectId = "")
        {
            var stream = file.OpenReadStream();
            var contentType = file.ContentType;
            var suffix = file.FileName.GetFileType();
            var image = new SysFileService().UploadFile(stream, suffix, fileType, contentType, objectId);

            return MapperHelper.Map<ImageInfo>(image);
        }

        /// <summary>
        /// 上传图片，自动生成预览图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public IEnumerable<ImageInfo> UploadBigImage([FromForm] IFormFile file, [FromQuery]string fileType, [FromQuery]string objectId = "")
        {
            return new SysFileService().UploadBigImage(file, fileType, objectId);
        }
    }
}