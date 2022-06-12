using Sixpence.ORM.Entity;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.RecommendInfo
{
    public class RecommendInfoController : EntityBaseController<recommend_info, RecommendInfoService>
    {
        [HttpGet, AllowAnonymous, Route("data")]
        public override recommend_info GetData(string id)
        {
            return base.GetData(id);
        }

        [HttpGet, AllowAnonymous, Route("GetRecommendList")]
        public IList<recommend_info> GetRecommendList(string type = "url")
        {
            return new RecommendInfoService().GetRecommendList(type);
        }

        /// <summary>
        /// 记录阅读次数
        /// </summary>
        /// <param name="blogId"></param>
        [HttpGet, AllowAnonymous, Route("RecordReadingTimes")]
        public void RecordReadingTimes(string id)
        {
            new RecommendInfoService().RecordReadingTimes(id);
        }
    }
}
