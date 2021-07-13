using Blog.Core.Data;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.idea
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
        public override DataModel<idea> GetViewData(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            return base.GetViewData(searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
        }
    }
}
