using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Models
{
    /// <summary>
    /// 字段
    /// </summary>
    public class Column
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 逻辑名
        /// </summary>
        public string LogicalName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public DataType Type { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int? Length { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool? IsRequire { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue { get; set; }
    }
}
