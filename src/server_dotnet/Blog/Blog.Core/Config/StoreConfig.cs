using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Config
{
    public class StoreConfig : ConfigBase<StoreConfig>
    {
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
    }
}
