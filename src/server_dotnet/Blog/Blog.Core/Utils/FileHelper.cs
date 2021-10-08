using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blog.Core.Utils
{
    public static class FileHelper
    {
        /// <summary>
        /// 获取文件夹下文件
        /// </summary>
        /// <param name="directory">目录</param>
        /// <param name="folderType">文件类型</param>
        /// <param name="searchOption">搜索参数</param>
        /// <returns></returns>
        public static IList<string> GetFileList(string directory, FolderType folderType = FolderType.Bin, SearchOption searchOption = SearchOption.AllDirectories)
        {
            var path = folderType.GetPath();
            if (!Directory.Exists(path))
            {
                return new List<string>();
            }
            return Directory.GetFiles(path, directory, searchOption);
        }
    }
}
