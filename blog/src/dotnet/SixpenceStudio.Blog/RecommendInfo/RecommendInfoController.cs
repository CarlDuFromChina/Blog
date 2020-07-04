using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Blog.RecommendInfo
{
    [RequestAuthorize]
    public class RecommendInfoController : EntityBaseController<recommend_info, RecommendInfoService>
    {
        [HttpGet, AllowAnonymous]
        public override recommend_info GetData(string id)
        {
            return base.GetData(id);
        }

        [HttpGet, AllowAnonymous]
        public IList<recommend_info> GetRecommendList()
        {
            return new RecommendInfoService().GetRecommendList();
        }

        /// <summary>
        /// 记录阅读次数
        /// </summary>
        /// <param name="blogId"></param>
        [HttpGet, AllowAnonymous]
        public void RecordReadingTimes(string id)
        {
            new RecommendInfoService().RecordReadingTimes(id);
        }
    }
}
