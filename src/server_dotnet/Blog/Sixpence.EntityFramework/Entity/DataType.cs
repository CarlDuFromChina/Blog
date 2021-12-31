using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sixpence.EntityFramework.Entity
{
    public enum DataType
    {
        /// <summary>
        /// 文本，有长度限制
        /// </summary>
        Varchar,
        /// <summary>
        /// 文本，无长度限制（推荐）
        /// </summary>
        Text,
        /// <summary>
        /// 日期时间（无时区）
        /// </summary>
        Timestamp,
        /// <summary>
        /// 日期
        /// </summary>
        Date,
        /// <summary>
        /// 时间
        /// </summary>
        Time,
        /// <summary>
        /// integer，-2147483648 到 +2147483647
        /// </summary>
        Int4,
        /// <summary>
        /// integer，64位翻倍
        /// </summary>
        Int8,
        /// <summary>
        /// 小数点前 131072 位；小数点后 16383 位
        /// </summary>
        Numeric,
        /// <summary>
        /// 快取慢存（推荐，支持索引）
        /// </summary>
        Jsonb,
        /// <summary>
        /// 慢取快存
        /// </summary>
        Json,
        /// <summary>
        /// 布尔，true、false、unknown
        /// </summary>
        Boolean
    }

    public static class DataTypeExtension
    {
        /// <summary>
        /// 转换成C#类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToCSharpType(this DataType type)
        {
            switch (type)
            {
                case DataType.Varchar:
                case DataType.Text:
                    return "string";
                case DataType.Timestamp:
                case DataType.Date:
                    return "DateTime?";
                case DataType.Time:
                    return "TimeSpan";
                case DataType.Int4:
                    return "int?";
                case DataType.Int8:
                    return "long?";
                case DataType.Numeric:
                    return "Decimal?";
                case DataType.Jsonb:
                case DataType.Json:
                    return "JToken";
                case DataType.Boolean:
                    return "bool";
                default:
                    throw new NotSupportedException("类型不支持");
            }
        }

        public static object Convert(string value, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Varchar:
                case DataType.Text:
                case DataType.Timestamp:
                case DataType.Date:
                case DataType.Time:
                    return value;
                case DataType.Int4:
                case DataType.Int8:
                    return ConvertUtil.ConToInt(value);
                case DataType.Numeric:
                    return ConvertUtil.ConToDecimal(value);
                case DataType.Jsonb:
                case DataType.Json:
                    return value;
                case DataType.Boolean:
                    return ConvertUtil.ConToBoolean(value);
                default:
                    throw new NotSupportedException("类型不支持");
            }
        }
    }

}
