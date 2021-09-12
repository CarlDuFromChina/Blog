using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Config
{
    public class SwaggerConfig : ConfigBase<SwaggerConfig>
    {
        /// <summary>
        /// 开关
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 路由前缀
        /// </summary>
        public string RoutePrefix { get; set; }
    }
}
