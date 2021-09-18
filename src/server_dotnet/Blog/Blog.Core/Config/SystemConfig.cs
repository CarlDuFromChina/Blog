using Sixpence.Core.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Config
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfig : BaseAppConfig<SystemConfig>
    {
        /// <summary>
        /// 默认密码
        /// </summary>
        public string DefaultPassword { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 协议
        /// </summary>
        public string Protocol { get; set; }
    }
}
