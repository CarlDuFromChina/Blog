using Sixpence.Common;
using Sixpence.Common.IoC;
using Sixpence.ORM.Broker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Blog.Sync
{
    [ServiceRegister]
    public interface ISyncBlog
    {
        /// <summary>
        /// 数据库持久化
        /// </summary>
        IPersistBroker Broker { get; set; }

        /// <summary>
        /// 执行同步
        /// </summary>
        /// <param name="id">博客id</param>
        void Execute(string id);
    }
}
