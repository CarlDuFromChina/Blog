using Blog.Core.Data;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.FriendBlog
{
    public class FriendBlogController : EntityBaseController<friend_blog, FriendBlogService>
    {
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
        public override DataModel<friend_blog> GetViewData(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            return base.GetViewData(searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
        }

        [HttpGet, AllowAnonymous]
        public override friend_blog GetData(string id)
        {
            return base.GetData(id);
        }
    }
}
