using Sixpence.ORM.Entity;
using Sixpence.Web.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.ORM.Models;
using Blog.Business.Entity;
using Blog.Business.Service;

namespace Blog.Business.Controller
{
    public class RecommendInfoController : EntityBaseController<RecommendInfo, RecommendInfoService>
    {
        [HttpGet("{id}"), AllowAnonymous]
        public override RecommendInfo GetData(string id)
        {
            return base.GetData(id);
        }

        /// <summary>
        /// 记录阅读次数
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("reading_times"), AllowAnonymous]
        public void RecordReadingTimes(string id)
        {
            new RecommendInfoService().RecordReadingTimes(id);
        }

        [HttpGet("search"), AllowAnonymous]
        public override DataModel<RecommendInfo> GetViewData(string pageSize = "", string pageIndex = "", string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            return base.GetViewData(pageSize, pageIndex, searchList, orderBy, viewId, searchValue);
        }
    }
}
