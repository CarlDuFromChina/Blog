using Sixpence.Common;
using Sixpence.Common.IoC;
using Sixpence.ORM.EntityManager;
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
        /// 执行同步
        /// </summary>
        /// <param name="id">博客id</param>
        /// <param name="param">参数</param>
        void Execute(string id, object param);
    }
}
