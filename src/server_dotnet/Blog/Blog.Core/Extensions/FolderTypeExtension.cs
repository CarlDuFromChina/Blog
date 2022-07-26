using Blog.Core.Config;
using Sixpence.Common;
using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Core
{
    /// <summary>
    /// 系统文件扩展类
    /// </summary>
    public static class FolderTypeExtension
    {
        public static string storage = StoreConfig.Config.Storage;
        public static string temp = StoreConfig.Config.Temp;

        static FolderTypeExtension()
        {
            try
            {
                if (!Directory.Exists(temp))
                {
                    Directory.CreateDirectory(temp);
                }
                if (!Directory.Exists(storage))
                {
                    Directory.CreateDirectory(storage);
                }
            }
            catch
            {
                throw new SpException("文件目录初始化失败");
            }
        }

        public static string GetPath(this FolderType type)
        {
            var folderPath = AppDomain.CurrentDomain.BaseDirectory;
            switch (type)
            {
                case FolderType.Bin:
                    break;
                case FolderType.Log:
                    folderPath += $"{Path.AltDirectorySeparatorChar}log";
                    break;
                case FolderType.LogArchive:
                    folderPath += $"{Path.AltDirectorySeparatorChar}log{Path.AltDirectorySeparatorChar}Archive";
                    break;
                case FolderType.Temp:
                    folderPath = temp;
                    break;
                case FolderType.Storage:
                    folderPath = storage;
                    break;
                case FolderType.Version:
                    folderPath += $"{Path.AltDirectorySeparatorChar}version";
                    break;
                default:
                    break;
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }
    }

    /// <summary>
    /// 目录类型
    /// </summary>
    public enum FolderType
    {
        [Description("默认目录")]
        Default,
        [Description("dll目录")]
        Bin,
        [Description("日志目录")]
        Log,
        [Description("日志归档目录")]
        LogArchive,
        [Description("临时目录")]
        Temp,
        [Description("文件存储目录")]
        Storage,
        [Description("升级脚本")]
        Version
    }
}
