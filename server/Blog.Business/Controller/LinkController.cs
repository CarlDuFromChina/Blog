using Sixpence.Web.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sixpence.ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Business.Service;
using Blog.Business.Entity;

namespace Blog.Business.Controller
{
    public class LinkController : EntityBaseController<Link, LinkService>
    {
        [HttpGet("search"), AllowAnonymous]
        public override DataModel<Link> GetViewData(string pageSize = "", string pageIndex = "", string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            return base.GetViewData(pageSize, pageIndex, searchList, orderBy, viewId, searchValue);
        }
    }
}
