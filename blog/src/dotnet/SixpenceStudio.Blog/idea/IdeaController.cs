using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Blog.idea
{
    [RequestAuthorize]
    public class IdeaController : EntityController<idea, IdeaSerivice>
    {
        [HttpGet, AllowAnonymous]
        public override idea GetData(string id)
        {
            return base.GetData(id);
        }

        [HttpGet, AllowAnonymous]
        public override IList<idea> GetDataList(string searchList = "", string orderBy = "", string viewId = "")
        {
            return base.GetDataList(searchList, orderBy, viewId);
        }

        [HttpGet, AllowAnonymous]
        public override DataModel<idea> GetDataList(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "")
        {
            return base.GetDataList(searchList, orderBy, pageSize, pageIndex, viewId);
        }
    }
}
