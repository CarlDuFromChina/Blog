using Blog.Business.Entity;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using Sixpence.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Service
{
    public class RecommendInfoService : EntityService<RecommendInfo>
    {
        #region 构造函数
        public RecommendInfoService() : base() { }

        public RecommendInfoService(IEntityManager manager) : base(manager) { }
        #endregion

        /// <summary>
        /// 记录阅读次数
        /// </summary>
        /// <param name="id">id</param>
        public void RecordReadingTimes(string id)
        {
            var sql = @"
UPDATE recommend_info SET reading_times = COALESCE(reading_times, 0) + 1 WHERE id = @id
";
            Manager.Execute(sql, new Dictionary<string, object>() { { "@id", id } });
        }
    }
}
