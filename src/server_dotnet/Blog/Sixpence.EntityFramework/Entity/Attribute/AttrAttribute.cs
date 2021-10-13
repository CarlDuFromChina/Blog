using Sixpence.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Entity
{
    /// <summary>
    /// 字段特性（映射数据库字段类型）
    /// </summary>
    public sealed class AttrAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="logicalName">字段逻辑名</param>
        /// <param name="type">字段类型</param>
        /// <param name="length">字段长度</param>
        /// <param name="isRequire">是否必填</param>
        public AttrAttribute(string name, string logicalName, DataType type, int length, bool isRequire = false)
        {
            this.Attr = new Column()
            {
                Name = name,
                LogicalName = logicalName,
                Type = type,
                Length = length,
                IsRequire = isRequire
            };
        }

        public AttrAttribute(string name, string logicalName, DataType type, bool isRequire = false)
        {
            this.Attr = new Column()
            {
                Name = name,
                LogicalName = logicalName,
                Type = type,
                IsRequire = isRequire
            };
        }

        public Column Attr { get; set; }
    }
}
