using SixpenceStudio.Core;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.RecommendInfo
{
    public class RecommendInfoService : EntityService<recommend_info>
    {
        #region 构造函数
        public RecommendInfoService()
        {
            _cmd = new EntityCommand<recommend_info>();
        }

        public RecommendInfoService(IPersistBroker Broker)
        {
            _cmd = new EntityCommand<recommend_info>(Broker);
        }
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
	LIMIT 5
";
            var paramList = new Dictionary<string, object>() { { "@type", type } };
            var data = _cmd.Broker.RetrieveMultiple<recommend_info>(sql, paramList);
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
            _cmd.Broker.Execute(sql, new Dictionary<string, object>() { { "@id", id } });
        }
    }
}
