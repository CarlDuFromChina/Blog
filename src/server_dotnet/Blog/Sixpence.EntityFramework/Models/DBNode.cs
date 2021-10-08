using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sixpence.EntityFramework.Models
{

    public class DBNode
    {
        public string DriverType { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// 数据库类型（主从）
    /// </summary>
    public enum DbType
    {
        /// <summary>
        /// 主库（负责读和写）
        /// </summary>
        [Description("主库")]
        Main,
        /// <summary>
        /// 从库（负责读）
        /// </summary>
        [Description("从库")]
        StandBy
    }
}
