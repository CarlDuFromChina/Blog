using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sixpence.EntityFramework.Entity
{
    /// <summary>
    /// 字段类型枚举
    /// </summary>
    public enum AttrType
    {
        [Description("varchar")]
        Varchar,
        [Description("timestamp")]
        Timestamp,
        [Description("INT4")]
        Int4,
        [Description("INT8")]
        Int8,
        [Description("numeric")]
        Decimal,
        [Description("jsonb")]
        JToken,
        [Description("text")]
        Text
    }
}
