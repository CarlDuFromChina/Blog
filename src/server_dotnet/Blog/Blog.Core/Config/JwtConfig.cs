using Sixpence.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Config
{
    public class JwtConfig : ConfigBase<JwtConfig>
    {
        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 可以给哪些客户端使用
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 刷新颁布者
        /// </summary>
        public string RefreshAudience { get; set; }

        /// <summary>
        /// 加密
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int ExpireSeconds { get; set; }

        /// <summary>
        /// 刷新过期时间
        /// </summary>
        public int RefreshExpireHours { get; set; }
    }
}
