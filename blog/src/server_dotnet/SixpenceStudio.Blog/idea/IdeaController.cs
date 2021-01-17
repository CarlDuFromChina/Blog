using SixpenceStudio.Core;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Blog.idea
{
    public class IdeaController : EntityBaseController<idea, IdeaSerivice>
    {
        [HttpGet, AllowAnonymous]
        public override idea GetData(string id)
        {
            return base.GetData(id);
        }

        [HttpGet, AllowAnonymous]
        public override IList<idea> GetDataList(string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            return base.GetDataList(searchList, orderBy, viewId, searchValue);
        }

        [HttpGet, AllowAnonymous]
        public override DataModel<idea> GetDataList(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            return base.GetDataList(searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
        }
    }
}
