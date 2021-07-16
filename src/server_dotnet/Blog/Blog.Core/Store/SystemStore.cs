using Blog.Core.Auth.Role;
using Blog.Core.Data;
using Blog.Core.Logging;
using Blog.Core.Store.SysFile;
using Blog.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Store
{
    public class SystemStore : IStoreStrategy
    {
        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete(IList<string> fileName)
        {
            fileName.ToList().ForEach(item =>
            {
                var filePath = Path.Combine(FolderType.Storage.GetPath(), item);
                FileUtil.DeleteFile(filePath);
            });
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        public async Task<IActionResult> DownLoad(string objectId)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var data = broker.Retrieve<sys_file>(objectId) ?? broker.Retrieve<sys_file>("select * from sys_file where hash_code = @id", new Dictionary<string, object>() { { "@id", objectId } });
            var fileInfo = new FileInfo(Path.Combine(FolderType.Storage.GetPath(), data?.name ?? ""));
            if (fileInfo.Exists)
            {
                var stream = await FileUtil.GetFileStreamAsync(fileInfo.FullName);
                HttpContext.Current.Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileInfo.Name);
                return new FileStreamResult(stream, "application/octet-stream");
            }
            LogUtils.Error($"文件{fileInfo.Name}未找到，文件路径：{fileInfo.FullName}");
            throw new FileNotFoundException();
        }

        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Stream GetStream(string id)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var data = broker.Retrieve<sys_file>(id);
            return FileUtil.GetFileStream(Path.Combine(FolderType.Storage.GetPath(), data.name));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public void Upload(Stream stream, string fileName, out string filePath)
        {
            filePath = $"\\storage\\{fileName}"; // 相对路径
            var path = Path.Combine(FolderType.Storage.GetPath(), fileName); // 绝对路径
            FileUtil.SaveFile(stream, path);
        }
    }
}
