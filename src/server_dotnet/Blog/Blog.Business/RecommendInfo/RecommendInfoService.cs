
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.RecommendInfo
{
    public class RecommendInfoService : EntityService<recommend_info>
    {
        #region 构造函数
        public RecommendInfoService() : base() { }

        public RecommendInfoService(IEntityManager manager) : base(manager) { }
        #endregion

        /// <summary>
        /// 获取推荐信息
        /// </summary>
        /// <returns></returns>
        public IList<recommend_info> GetRecommendList(string type = "url")
        {
            var sql = $@"
SELECT
	* 
FROM
	recommend_info 
WHERE
	recommend_type = @type 
ORDER BY
	createdon DESC 
";
            Manager.DbClient.Driver.AddLimit(ref sql, null, 5);
            var paramList = new Dictionary<string, object>() { { "@type", type } };
            var data = Manager.Query<recommend_info>(sql, paramList).ToList();
            return data;
        }

        /// <summary>
        /// 记录阅读次数
        /// </summary>
        /// <param name="id">id</param>
        public void RecordReadingTimes(string id)
        {
            var sql = @"
UPDATE recommend_info SET reading_times = COALESCE(reading_times, 0) + 1 WHERE recommend_infoid = @id
";
            Manager.Execute(sql, new Dictionary<string, object>() { { "@id", id } });
        }
    }
}
