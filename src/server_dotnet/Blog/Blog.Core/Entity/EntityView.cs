using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.ORM.Entity
{
    /// <summary>
    /// 实体试图
    /// </summary>
    public class EntityView
    {
        /// <summary>
        /// 视图名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 快速搜索筛选
        /// </summary>
        public IList<string> CustomFilter { get; set; }

        /// <summary>
        /// 视图Id
        /// </summary>
        public string ViewId { get; set; }

        /// <summary>
        /// 视图Sql
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string OrderBy { get; set; }
    }
}
