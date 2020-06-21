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
        public override IList<recommend_info> GetDataList(string searchList = "", string orderBy = "", string viewId = "")
        {
            return base.GetDataList(searchList, orderBy, viewId);
        }

        [HttpGet, AllowAnonymous]
        public override DataModel<recommend_info> GetDataList(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "")
        {
            return base.GetDataList(searchList, orderBy, pageSize, pageIndex, viewId);
        }
    }
}
