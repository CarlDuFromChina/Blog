using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Data
{
    [ServiceRegister]
    public interface IEntity
    {
        /// <summary>
        /// 获取实体名（表名）
        /// </summary>
        /// <returns></returns>
        string GetEntityName();

        /// <summary>
        /// 获取逻辑名
        /// </summary>
        /// <returns></returns>
        string GetLogicalName();

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="attributeLogicalName"></param>
        /// <returns></returns>
        object GetAttributeValue(string attributeLogicalName);

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="attributeLogicalName"></param>
        /// <param name="value"></param>
        void SetAttributeValue(string attributeLogicalName, object value);

        /// <summary>
        /// 是否是系统实体
        /// </summary>
        /// <returns></returns>
        bool IsSystemEntity();

        /// <summary>
        /// 获取实体所有字段
        /// </summary>
        /// <returns></returns>
        IEnumerable<Column> GetAttrs();
    }
}
