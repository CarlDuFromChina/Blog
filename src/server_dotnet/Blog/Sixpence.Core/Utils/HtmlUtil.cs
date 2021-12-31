using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Sixpence.Common.Utils
{
    public static class HtmlUtil
    {
        ///<summary>
        /// 取得HTML中所有图片的 URL。
        ///</summary>
        ///<param name="sHtmlText">HTML代码</param>
        ///<returns>图片的URL列表</returns>
        public static List<string> GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(sHtmlText);

            List<string> sUrlList = new List<string>();

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList.Add(match.Groups["imgUrl"].Value);

            return sUrlList;
        }

        ///<summary>
        /// 取得HTML中所有图片标签
        ///</summary>
        ///<param name="sHtmlText">HTML代码</param>
        ///<returns>图片的URL列表</returns>
        public static List<string> GetHtmlImagelList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(sHtmlText);

            List<string> sUrlList = new List<string>();

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList.Add(match.Value);

            return sUrlList;
        }
    }
}
