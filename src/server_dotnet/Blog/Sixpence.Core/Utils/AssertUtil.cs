using Sixpence.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.Common.Utils
{
    /// <summary>
    /// 断言
    /// </summary>
    public static class AssertUtil
    {
        /// <summary>
        /// 检查是否为真
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorId"></param>
        public static void CheckBoolean<T>(bool result, string errorMessage, string errorId)
            where T : Exception
        {
            if (result)
            {
                Assert<T>(errorMessage, errorId);
            }
        }

        /// <summary>
        /// 检查是否是Null或空字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorId"></param>
        public static void CheckIsNullOrEmpty<T>(string value, string errorMessage, string errorId)
            where T : Exception
        {
            if (string.IsNullOrEmpty(value))
            {
                Assert<T>(errorMessage, errorId);
            }
        }

        /// <summary>
        /// 检查是否是null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="errorMessage"></param>
        /// <param name="errorId"></param>
        public static void CheckNull<T>(object value, string errorMessage, string errorId) where T : Exception
        {
            CheckBoolean<T>(value == null, errorMessage, errorId);
        }

        private static void Assert<T>(string errorMessage, string errorId) where T : Exception
        {
            var ex = Activator.CreateInstance(typeof(T), errorMessage) as T;
            if (ex != null)
            {
                LogUtils.Error($"{errorId}：{errorMessage}");
                throw ex;
            }
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool CheckEmpty(params string[] args)
        {
            return args.Any(item => string.IsNullOrEmpty(item));
        }
    }
}
