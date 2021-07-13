using Blog.Core.Auth;
using Blog.Core.Config;
using Blog.Core.Store;
using Blog.Core.Store.SysFile;
using Blog.Core.Utils;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.DataService
{
    public class DataServiceController : BaseApiController
    {
        /// <summary>
        /// 获取公钥
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public string GetPublicKey()
        {
            return new DataService().GetPublicKey();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ImageInfo UploadImage([FromForm]IFormFile file, [FromQuery] string fileType, [FromQuery] string objectId)
        {
            var stream = file.OpenReadStream();

            // 获取文件哈希码，将哈希码作为文件名
            var hash_code = SHAUtil.GetFileSHA1(stream);
            var fileName = $"{hash_code}.{file.FileName.GetFileType()}";

            // 保存图片到本地
            // TODO：执行失败回滚操作
            var config = StoreConfig.Config;
            ServiceContainer.Resolve<IStoreStrategy>(config.Type).Upload(stream, fileName, out var filePath);
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

            return new ImageInfo()
            {
                id = sysImage.sys_fileId,
                name = sysImage.name,
                downloadUrl = $"api/SysFile/Download?objectId={sysImage.sys_fileId}"
            };
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <returns>图片源</returns>
        [HttpGet, AllowAnonymous]
        public string GetRandomImage()
        {
            return new DataService().GetRandomImage();
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public bool Test()
        {
            var token = Core.HttpContext.Current.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            else
            {
                return JwtHelper.SerializeJwt(token) != null;
            }
        }
    }
}