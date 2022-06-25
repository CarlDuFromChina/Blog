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
    }
}
