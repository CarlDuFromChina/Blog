using Sixpence.EntityFramework.Entity;
using Sixpence.EntityFramework.SelectOption;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Auth.Role
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public static class OperationTypeValue
    {
        /// <summary>
        /// 写
        /// </summary>
        public static SelectOption Write => new SelectOption("写", "write");

        /// <summary>
        /// 删
        /// </summary>
        public static SelectOption Delete => new SelectOption("删", "delete");

        /// <summary>
        /// 读
        /// </summary>
        public static SelectOption Read => new SelectOption("读", "read");
    }

    /// <summary>
    /// 操作类型枚举
    /// </summary>
    public enum OperationType
    {
        [Description("读")]
        Read = 1,
        [Description("写")]
        Write = 2,
        [Description("删")]
        Delete = 4
    }
}
