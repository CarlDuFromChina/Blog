using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
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

        public RecommendInfoService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<recommend_info>(broker);
        }
        #endregion

        /// <summary>
        /// 获取推荐信息
        /// </summary>
        /// <returns></returns>
        public IList<recommend_info> GetRecommendList()
        {
            var sql = $@"
SELECT
	* 
FROM
	recommend_info 
WHERE
	recommend_type = 'url' 
ORDER BY
	createdon DESC 
	LIMIT 5
";
            var data = _cmd.broker.RetrieveMultiple<recommend_info>(sql);
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
            _cmd.broker.Execute(sql, new Dictionary<string, object>() { { "@id", id } });
        }

    }
}
