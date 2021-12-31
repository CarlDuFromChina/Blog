using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.Common
{
    public static class DataTableExtension
    {
        /// <summary>
        /// 是否是个空表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsEmpty(this DataTable dt)
        {
            return dt == null || dt.Rows.Count == 0;
        }
    }
}
