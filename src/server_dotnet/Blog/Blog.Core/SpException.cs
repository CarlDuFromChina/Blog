using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core
{
    public class SpException : ApplicationException
    {
        #region 构造函数
        public SpException() { }
        public SpException(string message)
        {
            this.message = message;
            this.messageId = Guid.NewGuid().ToString();
        }
        public SpException(string message, string messageId = "")
        {
            this.message = message;
            this.messageId = string.IsNullOrEmpty(messageId) ? Guid.NewGuid().ToString() : messageId;
        }
        #endregion


        /// <summary>
        /// 错误提示
        /// </summary>
        private string message;
        public override string Message => message;

        /// <summary>
        /// 错误提示的 Id
        /// </summary>
        private string messageId;
        public string MessageId => messageId;
    }
}
