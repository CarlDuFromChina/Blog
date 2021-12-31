using System;
using System.Collections.Generic;
using System.Text;

namespace Sixpence.Common.Utils
{
    public class ConvertUtil
    {
        public static int ConToInt(object value)
        {
            if (value == null || value is DBNull || value.ToString().Trim() == "") return 0;

            if (value is int)
                return (int)value;

            return Convert.ToInt32(value);
        }

        public static string ConToString(object value)
        {
            return (value == null || value is DBNull) ? string.Empty : value.ToString();
        }

        public static byte[] ConToBytes(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new byte[0];
            }

            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        public static bool ConToBoolean(object value)
        {
            if (value == null || value is DBNull || value.ToString().Trim() == "")
            {
                return false;
            }

            if (value is string)
            {
                var s = value.ToString();
                return s == "1";
            }

            return Convert.ToBoolean(value);
        }

        public static double ConToDouble(object value)
        {
            if (value == null || value is DBNull || value.ToString().Trim() == "")
                return 0d;

            return Convert.ToDouble(value);
        }

        public static decimal ConToDecimal(object value)
        {
            if (value == null || value is DBNull || value.ToString().Trim() == "")
                return 0M;

            return Convert.ToDecimal(value);
        }

        public static DateTime ConToDateTime(object date)
        {
            if (date == null || date is DBNull)
                return default(DateTime);

            if (date is DateTime)
                return (DateTime)date;

            try
            {
                return Convert.ToDateTime(date.ToString());
            }
            catch
            {
                return default(DateTime);
            }
        }
    }
}
