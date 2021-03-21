using SixpenceStudio.Core;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Blog.Blog
{
    public class BlogController : EntityBaseController<blog, BlogService>
    {
        /// <summary>
        /// 查询所有博客菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> GetBlogRouterNameList()
        {
            return new BlogService().GetBlogRouterNameList();
        }

        /// <summary>
        /// 获取所有博客
        /// </summary>
        /// <param name="searchList"></param>
        /// <param name="orderBy"></param>
        /// <param name="viewId"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public override IList<blog> GetDataList(string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            return base.GetDataList(searchList, orderBy, viewId, searchValue);
        }

        /// <summary>
        /// 分页获取博客
        /// </summary>
        /// <param name="searchList"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="viewId"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public override DataModel<blog> GetDataList(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            return base.GetDataList(searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
        }

        /// <summary>
        /// 获取博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public override blog GetData(string id)
        {
            return base.GetData(id);
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="blogId"></param>
        [HttpGet, AllowAnonymous]
        public void Upvote(string blogId)
        {
            new BlogService().Upvote(blogId);
        }

        [HttpPost]
        public void SyncToWeChat([FromUri]string id, [FromBody]string htmlContent)
        {
            var content = HttpUtility.UrlDecode(htmlContent, Encoding.UTF8);
            new BlogService().SyncToWeChat(id, content);
        }
    }
}
