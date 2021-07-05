using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Auth
{
    /// <summary>
    /// 登录返回结果
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool result { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public ComplexToken token { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }
    }
}