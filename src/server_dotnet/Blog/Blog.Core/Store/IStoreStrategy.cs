using Microsoft.AspNetCore.Mvc;
using Sixpence.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Store
{
    /// <summary>
    /// 存储策略接口
    /// </summary>
    [ServiceRegister]
    public interface IStoreStrategy
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        void Upload(Stream stream, string fileName, out string filePath);

        /// <summary>
        /// 下载文件
        /// </summary>
        Task<IActionResult> DownLoad(string objectId);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        void Delete(IList<string> fileName);

        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Stream GetStream(string id);
    }
}
