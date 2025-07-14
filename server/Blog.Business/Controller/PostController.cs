using System;
using System.Collections.Generic;
using Sixpence.Web.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sixpence.Common.Utils;
using Sixpence.ORM.Models;
using Blog.Business.Service;
using Blog.Business.Model;
using Blog.Business.Entity;
using Sixpence.Web.Entity;
using Newtonsoft.Json;

namespace Blog.Business.Controller
{
    public class PostController : EntityBaseController<Post, PostService>
    {
        /// <summary>
        /// 查询所有博客菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("routers")]
        public IEnumerable<string> GetPostRouters()
        {
            return new PostService().GetPostRouters();
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
        [HttpGet("search"), AllowAnonymous]
        public override DataModel<Post> GetViewData(string pageSize = "", string pageIndex = "", string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            return base.GetViewData(pageSize, pageIndex, searchList, orderBy, viewId, searchValue);
        }

        /// <summary>
        /// 获取博客分类数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories"), AllowAnonymous]
        public PostCategories categories()
        {
            return new PostService().GetCategories();
        }

        /// <summary>
        /// 获取博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), AllowAnonymous]
        public override Post GetData(string id)
        {
            return MemoryCacheUtil.GetOrAddCacheItem(id, () => base.GetData(id), DateTime.Now.AddHours(2));
        }

        /// <summary>
        /// 同步博客
        /// </summary>
        /// <param name="id">博客id</param>
        /// <param name="destination">目标系统，例如：Juejin、WeChat</param>
        /// <param name="param">参数</param>
        [HttpPost, Route("sync")]
        public void SyncPost(string id, string destination, [FromBody] object param)
        {
            new PostService().SyncPost(id, destination, param);
        }

        /// <summary>
        /// 导出Markdown
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("export/markdown/{id}")]
        public IActionResult ExportMarkdown(string id)
        {
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            var result = new PostService().ExportMarkdown(id);
            return File(result.bytes, result.ContentType, result.fileName);
        }

        /// <summary>
        /// 获取主页用户
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, Route("index_user")]
        public UserInfo GetIndexUser()
        {
            return new PostService().GetIndexUser();
        }

        [HttpPost("save")]
        public override string CreateOrUpdateData(Post entity)
        {
            var id = base.CreateOrUpdateData(entity);
            return JsonConvert.SerializeObject(id);
        }

        [HttpPost]
        public override string CreateData(Post entity)
        {
            var id = base.CreateData(entity);
            return JsonConvert.SerializeObject(id);
        }
    }
}
