using Sixpence.Core.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Blog.Core.Config
{
    public class StoreConfig
    {
        public static StoreConfig Config { get; private set; }

        static StoreConfig()
        {
            Config = new JsonConfig().GetConfig<StoreConfig>(nameof(StoreConfig).Replace("Config", ""));
            if (string.IsNullOrEmpty(Config.Temp))
            {
                Config.Temp = GetTempPath();
            }
            if (string.IsNullOrEmpty(Config.Storage))
            {
                Config.Storage = GetStoragePath();
            }
        }

        /// <summary>
        /// 存储方式（SystemStore、MinIOStore）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 临时文件路径
        /// </summary>
        public string Temp { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Storage { get; set; }


        private static string GetTempPath()
        {
            return GetCurrentSystemPath("temp");
        }

        private static string GetStoragePath()
        {
            return GetCurrentSystemPath("storage");
        }

        private static string GetCurrentSystemPath(string folderName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return $"C://{folderName}";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
            }

            return "";
        }
    }
}
