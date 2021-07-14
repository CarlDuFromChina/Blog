using Blog.Core.Data;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Draft
{
    public class DraftService : EntityService<draft>
    {
        #region 构造函数
        public DraftService()
        {
            this._context = new EntityContext<draft>();
        }

        public DraftService(IPersistBroker Broker)
        {
            this._context = new EntityContext<draft>(Broker);
        }
        #endregion

        public IList<draft> GetDrafts()
        {
            var sql = @"
SELECT * FROM draft
WHERE blogid NOT IN (
	SELECT blogid FROM blog
)
";
            return Broker.RetrieveMultiple<draft>(sql);
        }

        /// <summary>
        /// 根据博客id获取草稿
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public draft GetDataByBlogId(string blogId)
        {
            var sql = @"
SELECT * FROM draft
WHERE blogid = @blogid
";
            return Broker.Retrieve<draft>(sql, new Dictionary<string, object>() { { "@blogid", blogId } });
        }

        /// <summary>
        /// 根据博客id删除草稿
        /// </summary>
        /// <param name="blogId"></param>
        public void DeleteDataByBlogId(string blogId)
        {
            var sql = @"
DELETE FROM draft
WHERE blogid = @blogid
";
            Broker.Execute(sql, new Dictionary<string, object>() { { "@blogid", blogId } });
        }
    }
}
