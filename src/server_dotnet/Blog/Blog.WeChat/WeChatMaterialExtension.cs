using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Blog.WeChat
{
    public static class WeChatMaterialExtension
    {
        /// <summary>
        /// 获取素材类型字符串值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToMaterialTypeString(this MaterialType type)
        {
            switch (type)
            {
                case MaterialType.image:
                    return "image";
                case MaterialType.video:
                    return "video";
                case MaterialType.voice:
                    return "voice";
                case MaterialType.news:
                    return "news";
                default:
                    return "";
            }
        }
    }
}