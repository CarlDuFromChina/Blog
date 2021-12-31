using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.Common.Utils
{
    public sealed class MemoryCacheUtil
    {
        static readonly object _syn1 = new object(), _syn2 = new object();

        /// <summary>
        /// 使用键和值将某个缓存项插入缓存中，并指定基于时间的过期详细信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="seconds"></param>
        public static void Set(string key, object obj, int seconds = 7200)
        {
            MemoryCache.Default.Set(key, obj, new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(seconds)
            });
        }

        public static void Set(string key, object obj, DateTime expireDate)
        {
            MemoryCache.Default.Set(key, obj, new CacheItemPolicy()
            {
                AbsoluteExpiration = expireDate
            });
        }

        /// <summary>
        /// 取缓存项，如果不存在则返回空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCacheItem<T>(string key)
        {
            try
            {
                return (T)MemoryCache.Default[key];
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 是否包含指定键的缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Contains(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        /// <summary>
        /// 取缓存项,如果不存在则新增缓存项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cachePopulate"></param>
        /// <param name="absoluteExpiration">设置一个值，该值指示是否应在指定的时间点逐出缓存项</param>
        /// <param name="slidingExpiration">设置一个值，该值指示如果某个缓存项在给定时段内未被访问，是否应被逐出</param>
        /// <returns></returns>
        public static T GetOrAddCacheItem<T>(string key, Func<T> cachePopulate, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Invalid cache key");
            if (cachePopulate == null)
                throw new ArgumentNullException("cachePopulate");
            if (slidingExpiration == null && absoluteExpiration == null)
                throw new ArgumentException("Either a sliding expiration or absolute must be provided");

            if (MemoryCache.Default[key] == null)
            {
                lock (_syn1)
                {
                    if (MemoryCache.Default[key] == null)
                    {
                        T cacheValue = cachePopulate();
                        if (!typeof(T).IsValueType && ((object)cacheValue) == null)
                            return cacheValue;
                        var item = new CacheItem(key, cacheValue);
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);
                        MemoryCache.Default.Add(item, policy);
                    }
                }
            }

            return (T)MemoryCache.Default[key];
        }

        /// <summary>
        /// 取缓存项,如果不存在则新增缓存项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cachePopulate"></param>
        /// <param name="depeFilePath"></param>
        /// <returns></returns>
        public static T GetOrAddCacheItem<T>(string key, Func<T> cachePopulate, string depeFilePath)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Invalid cache key");
            if (cachePopulate == null)
                throw new ArgumentNullException("cachePopulate");

            if (MemoryCache.Default[key] == null)
            {
                lock (_syn2)
                {
                    if (MemoryCache.Default[key] == null)
                    {
                        T cacheValue = cachePopulate();
                        if (!typeof(T).IsValueType && ((object)cacheValue) == null)
                            return cacheValue;
                        var item = new CacheItem(key, cacheValue);
                        var policy = CreatePolicy(depeFilePath);
                        MemoryCache.Default.Add(item, policy);
                    }
                }
            }

            return (T)MemoryCache.Default[key];
        }

        /// <summary>
        /// 移除指定键的缓存项
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCacheItem(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();
            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }
            policy.Priority = System.Runtime.Caching.CacheItemPriority.Default;
            return policy;
        }

        private static CacheItemPolicy CreatePolicy(string filePath)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string>() { filePath }));
            policy.Priority = System.Runtime.Caching.CacheItemPriority.Default;
            return policy;
        }
    }

}
