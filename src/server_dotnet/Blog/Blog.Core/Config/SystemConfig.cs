using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Config
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfig : ConfigBase<SystemConfig>
    {
        /// <summary>
        /// 默认密码
        /// </summary>
        public string DefaultPassword { get; set; }
    }
}
