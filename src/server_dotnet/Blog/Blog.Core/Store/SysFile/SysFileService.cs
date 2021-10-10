﻿using Blog.Core.Config;
using Sixpence.EntityFramework.Entity;
using Sixpence.Core;
using Sixpence.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using Sixpence.EntityFramework.Broker;
using Microsoft.AspNetCore.Http;
using Blog.Core.Module.DataService;
using Blog.Core.Profiles;

namespace Blog.Core.Store.SysFile
{
    public class SysFileService : EntityService<sys_file>
    {
        #region 构造函数
        public SysFileService()
        {
            this._context = new EntityContext<sys_file>();
        }

        public SysFileService(IPersistBroker broker)
        {
            this._context = new EntityContext<sys_file>(broker);
        }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Name = "所有文件",
                    ViewId = "DD1D72FB-D7DE-49AC-B387-273375E6A7BA",
                    Sql = @"
SELECT
	sys_fileid,
	NAME,
	file_type,
	content_type,
	createdon,
	createdbyname
FROM
	sys_file
",
                    OrderBy = "createdon desc",
                    CustomFilter = new List<string>(){ "name" }
                },
                new EntityView()
                {
                    Name = "图库",
                    ViewId = "3BCF6C07-2B49-4D69-9EB1-A3D5B721C976",
                    Sql = $@"
SELECT
	sys_fileid,
	NAME,
	file_type,
	createdon,
	createdbyname,
	concat('{GetDownloadUrl("")}', sys_fileid) AS downloadUrl
FROM
	sys_file
",
                    OrderBy = "",
                    CustomFilter = new List<string>(){ "name" }
                }
            };
        }

        public IList<sys_file> GetDattaByCode(string code)
        {
            var sql = @"
SELECT * FROM sys_file
WHERE hash_code = @code
";
            return Broker.RetrieveMultiple<sys_file>(sql, new Dictionary<string, object>() { { "@code", code } });
        }

        public sys_file UploadFile(Stream stream, string fileSuffix, string fileType, string contentType, string objectId, string fileName = "")
        {
            var id = Guid.NewGuid().ToString();
            var hash_code = SHAUtil.GetFileSHA1(stream);
            var newFileName = $"{id}.{fileSuffix}"; // GUID 生成文件名

            // 保存图片到本地
            // TODO：执行失败回滚操作
            var config = StoreConfig.Config;
            ServiceContainer.Resolve<IStoreStrategy>(config.Type).Upload(stream, newFileName, out var filePath);

            var sysFile = new sys_file()
            {
                sys_fileId = id,
                name = fileName,
                real_name = newFileName,
                hash_code = hash_code,
                file_type = fileType,
                objectId = objectId,
                content_type = contentType,
                DownloadUrl = GetDownloadUrl(id)
            };
            CreateData(sysFile);

            return sysFile;
        }

        /// <summary>
        /// 上传大图并自动生成缩略图
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileType"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public IEnumerable<FileInfoModel> UploadBigImage(IFormFile file, string fileType, string objectId = "")
        {
            var stream = file.OpenReadStream();
            var contentType = file.ContentType;
            var suffix = file.FileName.GetFileType();

            return Broker.ExecuteTransaction(() =>
            {
                var image = UploadFile(stream, suffix, fileType, contentType, objectId, file.FileName);
                var thumbStream = ImageUtil.GetThumbnail(image.GetFilePath());
                var image2 = UploadFile(thumbStream, suffix, fileType, contentType, objectId);
                return new List<FileInfoModel>()
                {
                    MapperHelper.Map<FileInfoModel>(image),
                    MapperHelper.Map<FileInfoModel>(image2)
                };
            });
        }

        /// <summary>
        /// 获取本地下载地址
        /// </summary>
        /// <param name="fileid"></param>
        /// <returns></returns>
        public static string GetDownloadUrl(string fileid, bool isRelative = true)
        {
            if (isRelative)
            {
                return $"/api/SysFile/Download?objectId={fileid}";
            }
            var config = SystemConfig.Config;
            return $"{config.Protocol}://{config.Domain}/api/SysFile/Download?objectId={fileid}";
        }
    }
}