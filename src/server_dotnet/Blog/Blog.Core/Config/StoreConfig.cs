using Sixpence.Core.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Blog.Core.Config
{
    public class StoreConfig : BaseAppConfig<StoreConfig>
    {
        /// <summary>
        /// 存储方式（SystemStore、MinIOStore）
        /// </summary>
        public string Type { get; set; }

        private string temp { get; set; }
        /// <summary>
        /// 临时文件路径
        /// </summary>
        public string Temp
        {
            get
            {
                return string.IsNullOrEmpty(temp) ? GetCurrentSystemPath("temp") : temp;
            }
            set
            {
                temp = value;
            }
        }

        private string storage;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Storage
        {
            get
            {
                return string.IsNullOrEmpty(storage) ? GetCurrentSystemPath("storage") : storage;
            }
            set
            {
                storage = value;
            }
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
