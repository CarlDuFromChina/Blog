using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Draft
{
    public class DraftService : EntityService<draft>
    {
        #region 构造函数
        public DraftService()
        {
            this._cmd = new EntityCommand<draft>();
        }

        public DraftService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<draft>(broker);
        }
        #endregion

        public IList<draft> GetDataList()
        {
            var sql = @"
SELECT * FROM draft
WHERE blogid NOT IN (
	SELECT blogid FROM blog
)
";
            return _cmd.broker.RetrieveMultiple<draft>(sql);
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
            return _cmd.broker.Retrieve<draft>(sql, new Dictionary<string, object>() { { "@blogid", blogId } });
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
            _cmd.broker.Execute(sql, new Dictionary<string, object>() { { "@blogid", blogId } });
        }
    }
}
