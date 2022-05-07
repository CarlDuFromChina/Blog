using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth
{
    public class LoginRequest
    {
        public string code { get; set; }
        public string password { get; set; }
        public string publicKey { get; set; }
        public ThirdPartyLogin third_party_login { get; set; }
    }

    /// <summary>
    /// 第三方登录参数
    /// </summary>
    public class ThirdPartyLogin
    {
        /// <summary>
        /// 第三方登录参数
        /// </summary>
        public object param { get; set; }

        /// <summary>
        /// 第三方登录方式
        /// </summary>
        public ThirdPartyLoginType type { get; set; }
    }

    /// <summary>
    /// 第三方联合登录方式
    /// </summary>
    public enum ThirdPartyLoginType
    {
        Github,
        Gitee,
        QQ
    }
}
