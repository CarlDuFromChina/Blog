#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/20 22:17:30
Description：Long类型扩展类
********************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.Common
{
    public static class LongExtension
    {
        public static string ToDateTimeString(this long value, string format = "yyyy-MM-dd HH:mm")
        {
            return value.ToDateTime().ToString(format);
        }

        public static DateTime ToDateTime(this long value)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(value + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }
    }
}
