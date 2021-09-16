using Blog.Core.Config;
using Sixpence.EntityFramework.Entity;
using Sixpence.Core;
using Sixpence.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using Sixpence.EntityFramework.Broker;

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
                    Sql = @"
SELECT
	sys_fileid,
	NAME,
	createdon,
	createdbyname,
	concat('/api/SysFile/Download?objectId=', sys_fileid) AS downloadUrl
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

        public sys_file UploadFile(Stream stream, string fileSuffix, string fileType, string contentType, string objectId)
        {
            // 获取文件哈希码，将哈希码作为文件名
            var hash_code = SHAUtil.GetFileSHA1(stream);
            var newFileName = $"{hash_code}.{fileSuffix}";

            // 保存图片到本地
            // TODO：执行失败回滚操作
            var config = StoreConfig.Config;
            ServiceContainer.Resolve<IStoreStrategy>(config.Type).Upload(stream, newFileName, out var filePath);
            var sysImage = new sys_file()
            {
                sys_fileId = Guid.NewGuid().ToString(),
                name = newFileName,
                hash_code = hash_code,
                file_path = filePath,
                file_type = fileType,
                content_type = contentType,
            };
            sysImage.DownloadUrl = $"api/SysFile/Download?objectId={sysImage.sys_fileId}";

            if (!string.IsNullOrEmpty(objectId))
            {
                sysImage.objectId = objectId;
            }

            CreateData(sysImage);

            return sysImage;
        }
    }
}