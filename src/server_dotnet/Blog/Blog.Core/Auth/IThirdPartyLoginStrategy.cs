using Blog.Core.Auth;
using Sixpence.Common.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Auth
{
    /// <summary>
    /// 第三方联合登录接口
    /// </summary>
    [ServiceRegister]
    public interface IThirdPartyLoginStrategy
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param">传入参数，大部分情况是一个 code</param>
        /// <returns></returns>
        LoginResponse Login(object param);
    }
}
