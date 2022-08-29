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

namespace Blog.RecommendInfo
{
    public class RecommendInfoController : EntityBaseController<recommend_info, RecommendInfoService>
    {
        [HttpGet("{id}"), AllowAnonymous]
        public override recommend_info GetData(string id)
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
        public override DataModel<recommend_info> GetViewData(string pageSize = "", string pageIndex = "", string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            return base.GetViewData(pageSize, pageIndex, searchList, orderBy, viewId, searchValue);
        }
    }
}
