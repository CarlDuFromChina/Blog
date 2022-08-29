using Sixpence.Web.WebApi;
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
        [HttpGet("post/{postid}")]
        public draft GetDataByPostId(string postid)
        {
            return new DraftService().GetDataByPostId(postid);
        }

        /// <summary>
        /// 获取博客草稿（新建）
        /// </summary>
        /// <returns></returns>
        [HttpGet("drafts")]
        public IList<draft> GetDrafts()
        {
            return new DraftService().GetDrafts();
        }
    }
}
