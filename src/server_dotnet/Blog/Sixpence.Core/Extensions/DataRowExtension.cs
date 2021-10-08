using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sixpence.Core.Extensions
{
    public static class DataRowExtension
    {
        public static Dictionary<string, object> ToDictionary(this DataRow dataRow, DataColumnCollection columnCollection)
        {
            var columns = new List<string>();
            foreach (DataColumn column in columnCollection)
            {
                columns.Add(column.ColumnName);
            }
            var dic = new Dictionary<string, object>();
            for (int i = 0; i < columns.Count; i++)
            {
                dic.Add(columns[i], dataRow.ItemArray[i]);
            }
            return dic;
        }
    }
}
