using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.Common
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 判断字符串是否存在于一个字符串中
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// 转换Dictionary参数为日志文本格式
        /// </summary>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static string ToLogString(this Dictionary<string, object> paramList)
        {
            if (paramList == null || paramList.Count == 0)
            {
                return "\r\n";
            }
            var list = new List<string>();
            foreach (var item in paramList)
            {
                var str = $"{item.Key}: {item.Value}";
                list.Add(str);

            }
            return "\r\n" + string.Join("\r\n", list) + "\r\n";
        }

        /// <summary>
        /// 获取查找到的字符串之后的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="str">需要查找的字符串</param>
        /// <returns></returns>
        public static string GetSubString(this string value, string str)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            var startIndex = value.IndexOf(str);
            if (startIndex == -1 || startIndex == 0)
            {
                return "";
            }

            startIndex += str.Length;
            var len = value.Length - startIndex;
            return value.Substring(startIndex, len);
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetFileType(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var arr = value.Split('.');
                var typeName = arr[arr.Length - 1].ToString();
                return typeName;
            }
            return "";
        }

        /// <summary>
        /// 替换匹配字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public static string Replace(this string value, Dictionary<string, string> keyValuePairs)
        {
            if (string.IsNullOrEmpty(value) || keyValuePairs.IsEmpty())
            {
                return value;
            }

            keyValuePairs.Each(item => value = value.Replace(item.Key, item.Value));

            return value;
        }
    }
}
