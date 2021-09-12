using log4net;
using Newtonsoft.Json;
using Blog.Core.Logging;
using Blog.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Pixabay
{
    /// <summary>
    /// Pixabay第三方API资源（https://pixabay.com/api/docs/）
    /// </summary>
    public class PixabayService
    {
        public PixabayService()
        {
            logger = LogFactory.GetLogger("pixabay");
        }

        private static readonly string key = "19356383-2f75a9b525aa933f63ab20ab5";
        private static string Get(string url)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "Get";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36";
            request.ContentType = "application/json";
            var headerList = new Dictionary<string, string>() {
                { "Accept-Encoding", "gzip, deflate, br" },
                { "Accept-Language", "en-US,en;q=0.5" },
                { "Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9" }
            };
            if (headerList != null)
            {
                foreach (var header in headerList)
                {
                    if (header.Key.Equals("Accept", StringComparison.OrdinalIgnoreCase))
                    {
                        request.Accept = header.Value;
                    }
                    else
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
            }
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream == null) return string.Empty;

            if (response.Headers.Get("Content-Encoding") == "br")
            {
                responseStream = new BrotliStream(responseStream, CompressionMode.Decompress);
            }
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        private ILog logger;

        #region 获取图片资源
        public ImagesModel GetImages(string searchValue, int pageIndex, int pageSize)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return null;
            }
            var cache = MemoryCacheUtil.GetCacheItem<ImagesModel>($"PixabayApi_Images_{searchValue}_{pageIndex}_{pageSize}");
            if (cache != null) return cache;

            var url = string.Format("https://pixabay.com/api/?key={0}&q={1}&image_type=photo&lang=zh&page={2}&per_page={3}", key, searchValue, pageIndex, pageSize);
            try
            {
                logger.Debug($"Get：{url}");
                var result = Get(url);
                var resultJson = JsonConvert.DeserializeObject<ImagesModel>(result);
                // 缓存24小时
                MemoryCacheUtil.Set($"PixabayApi_Images_{searchValue}_{pageIndex}_{pageSize}", resultJson, 3600 * 24);
                return resultJson;
            }
            catch (Exception ex)
            {
                logger.Error($"获取图片资源：{searchValue} 失败", ex);
                throw ex;
            }
        }
        #endregion

        #region 获取视频资源 
        public VideosModel GetVideos(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return null;
            }
            var cache = MemoryCacheUtil.GetCacheItem<VideosModel>("PixabayApi_videos_" + searchValue);
            if (cache != null) return cache;

            var url = string.Format("https://pixabay.com/api/videos/?key={0}&q={1}", key, searchValue);
            try
            {
                logger.Debug($"Get：{url}");
                var result = Get(url);
                var resultJson = JsonConvert.DeserializeObject<VideosModel>(result);
                MemoryCacheUtil.Set("PixabayApi_videos_" + searchValue, resultJson, 3600 * 24);
                return resultJson;
            }
            catch (Exception ex)
            {
                logger.Error($"获取视频资源：{searchValue} 失败", ex);
                throw ex;
            }
        }
        #endregion

    }
}
