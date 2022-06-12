using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Draft
{
    public class DraftController : EntityBaseController<draft, DraftService>
    {
        /// <summary>
        /// 根据博客id获取草稿
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("GetDataByBlogId")]
        public draft GetDataByBlogId(string id)
        {
            return new DraftService().GetDataByBlogId(id);
        }

        /// <summary>
        /// 获取博客草稿（新建）
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetDrafts")]
        public IList<draft> GetDrafts()
        {
            return new DraftService().GetDrafts();
        }
    }
}
