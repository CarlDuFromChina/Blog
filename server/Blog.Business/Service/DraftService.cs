using Blog.Business.Entity;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using Sixpence.Web.Service;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Service
{
    public class DraftService : EntityService<Draft>
    {
        #region 构造函数
        public DraftService() : base() { }

        public DraftService(IEntityManager manager) : base(manager) { }
        #endregion

        public IList<Draft> GetDrafts()
        {
            var sql = @"
SELECT * FROM draft
WHERE postid NOT IN (
	SELECT postid FROM post
)
";
            return Manager.Query<Draft>(sql).ToList();
        }

        /// <summary>
        /// 根据博客id获取草稿
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Draft GetDataByPostId(string postId)
        {
            var sql = @"
SELECT * FROM draft
WHERE postid = @postid
";
            return Manager.QueryFirst<Draft>(sql, new Dictionary<string, object>() { { "@postid", postId } });
        }

        /// <summary>
        /// 根据博客id删除草稿
        /// </summary>
        /// <param name="postId"></param>
        public void DeleteDataByPostId(string postId)
        {
            var draft = Manager.QueryFirst<Draft>("select * from draft where postid = @id or id = @id", new { id = postId });
            if (draft != null)
                Manager.Delete(draft);
        }

        public override string CreateOrUpdateData(Draft t)
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
