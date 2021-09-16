using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Entity
{
    /// <summary>
    /// 主键自定义属性
    /// TODO：使用 Plugin 方式做主键唯一性检查
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,  AllowMultiple = true)]
    public sealed class KeyAttributesAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public KeyAttributesAttribute(string repeatMessage, params string[] attributes)
        {
            RepeatMessage = repeatMessage;
            AttributeList = new List<string>(attributes);
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string RepeatMessage { get; }

        /// <summary>
        /// 字段
        /// </summary>
        public IList<string> AttributeList { get; }
    }
}
