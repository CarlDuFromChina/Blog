using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Extensions
{
    public static class HttpResponseExtension
    {

        /// <summary>
        /// 自定义扩展方法输出
        /// </summary>
        /// <param name="response"></param>
        /// <param name="content"></param>
        public static async Task Write(this HttpResponse response, string content)
        {
            response.ContentType = "application/json";
            using (StreamWriter sw = new StreamWriter(response.Body))
            {
                await sw.WriteAsync(content);
            }
        }
    }
}
