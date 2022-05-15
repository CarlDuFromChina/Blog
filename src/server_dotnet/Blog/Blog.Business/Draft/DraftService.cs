
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
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
        public DraftService() : base() { }

        public DraftService(IEntityManager manager) : base(manager) { }
        #endregion

        public IList<draft> GetDrafts()
        {
            var sql = @"
SELECT * FROM draft
WHERE blogid NOT IN (
	SELECT blogid FROM blog
)
";
            return Manager.Query<draft>(sql).ToList();
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
            return Manager.QueryFirst<draft>(sql, new Dictionary<string, object>() { { "@blogid", blogId } });
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
            Manager.Execute(sql, new Dictionary<string, object>() { { "@blogid", blogId } });
        }

        public override string CreateOrUpdateData(draft t)
        {
            var data = Repository.FindOne(t.id);
            if (data != null)
            {
                t.created_by = data.created_by;
                t.created_by_name = data.created_by_name;
                t.created_at = data.created_at;
                Repository.Update(t);
            }
            else
            {
                return Repository.Create(t);
            }
            return t.id;
        }
    }
}
