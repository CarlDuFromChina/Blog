using Blog.Business.Upvote;
using Blog.Core;
using Sixpence.EntityFramework.Entity;
using Blog.Core.Module.Role;
using Blog.Core.Profiles;
using Sixpence.Core.Utils;
using Blog.WeChat;
using Blog.WeChat.Material;
using Blog.WeChat.WeChatNews;
using System;
using System.Collections.Generic;
using Sixpence.EntityFramework.Broker;
using Sixpence.Core;
using Newtonsoft.Json;
using Blog.Core.Config;
using System.IO;

namespace Blog.Business.Blog
{
    public class BlogService : EntityService<blog>
    {
        public const string BLOG_SURFACE_NAME = "blog_surface";
        public const string BLOG_CONTENT_NAME = "blog_content";

        #region 构造函数
        public BlogService()
        {
            _context = new EntityContext<blog>();
        }

        public BlogService(IPersistBroker broker)
        {
            _context = new EntityContext<blog>(broker);
        }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            var sql = $@"
SELECT
	blog.blogid,
	blog.title,
	blog.blog_type,
	blog.blog_typeName,
	blog.article_typeName,
	blog.createdBy,
	blog.createdbyname,
	blog.modifiedBy,
	blog.modifiedbyname,
	blog.createdOn,
	blog.modifiedOn,
	blog.is_series,
	blog.tags,
	COALESCE(blog.reading_times, 0) reading_times,
	COALESCE((SELECT COUNT(1) FROM upvote WHERE objectid = blog.blogid), 0) upvote_times,
	COALESCE((SELECT COUNT(1) FROM comments WHERE objectid = blog.blogid), 0) comment_count,
	blog.surfaceid,
	blog.surface_url,
	blog.brief,
	blog.is_pop,
	blog.is_popName
FROM
	blog
WHERE 1=1 AND blog.is_show = 1";
            var orderBy = "blog.is_pop DESC, blog.createdOn desc, blog.title, blog.blogid";
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = "SELECT * FROM blog",
                    ViewId = "C94EDAAE-0C59-41E6-A373-D4816C2FD882",
                    CustomFilter = new List<string>(){ "title" },
                    Name = "全部博客",
                    OrderBy = orderBy
                },
                new EntityView()
                {
                    Sql = sql + " AND blog.is_series = 0",
                    ViewId = "463BE7FE-5435-4841-A365-C9C946C0D655",
                    CustomFilter = new List<string>() { "title" },
                    Name = "展示的博客",
                    OrderBy = orderBy
                },
                new EntityView()
                {
                    Sql = sql + " AND blog.is_series = 1",
                    ViewId = "834F8083-47BC-42F3-A6B2-DE25BE755714",
                    CustomFilter = new List<string>() { "title" },
                    Name = "展示的系列",
                    OrderBy =orderBy
                },
                new EntityView()
                {
                    Sql = $@"SELECT * FROM blog WHERE blog.is_series = 1",
                    ViewId = "ACCE50D6-81A5-4240-BD82-126A50764FAB",
                    CustomFilter = new List<string>() { "title" },
                    Name = "全部系列",
                    OrderBy = orderBy
                }
            };
        }

        /// <summary>
        /// 根据id查询博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override blog GetData(string id)
        {
            return Broker.ExecuteTransaction(() =>
            {
                var data = base.GetData(id);
                var paramList = new Dictionary<string, object>() { { "@id", id } };
                data.upvote_times = Broker.QueryCount("SELECT COUNT(1) FROM upvote WHERE objectid = @id", paramList);
                data.comment_count = Broker.QueryCount("SELECT COUNT(1) FROM comments WHERE objectid = @id", paramList);
                Broker.Execute("UPDATE blog SET reading_times = COALESCE(reading_times, 0) + 1 WHERE blogid = @id", paramList);
                return data;
            });
        }

        /// <summary>
        /// 获取博客路由
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetBlogRouterNameList()
        {
            var sql = @"
SELECT
	router
FROM
	sys_menu 
WHERE
	parentid = '7EB12A4C-2698-4A8B-956D-B2467BE1D886'
";
            return Broker.DbClient.Query<string>(sql, param: null);
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="blogId"></param>
        public bool Upvote(string blogId)
        {
            var data = Broker.Retrieve<upvote>("SELECT * FROM upvote WHERE objectid = @id", new Dictionary<string, object>() { { "@id", blogId } });
            if (data != null)
            {
                Broker.Delete(data);
                return false;
            }
            else
            {
                var blog = Broker.Retrieve<blog>(blogId);
                data = new upvote()
                {
                    Id = Guid.NewGuid().ToString(),
                    objectId = blog.Id,
                    objectIdName = blog.title,
                    object_ownerid = blog.createdBy,
                    object_owneridName = blog.createdByName,
                    object_type = "blog"
                };
                Broker.Create(data);
                return true;
            }
        }

        /// <summary>
        /// 获取创作记录日历
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogActivityModel> GetActivity()
        {
            var sql = @"
SELECT
	to_char(createdon, 'YYYY-MM-DD') AS created_date,
	count(1) AS count
FROM blog
WHERE createdon > to_date(to_char(now(), 'YYYY-01-01'), 'YYYY-MM-DD') AND createdon < to_date(to_char(now(), 'YYYY-12-31'), 'YYYY-MM-DD')
GROUP BY to_char(createdon, 'YYYY-MM-DD')
";
            return Broker.Query<BlogActivityModel>(sql);
        }

        /// <summary>
        /// 导出Markdown
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (string fileName, string ContentType, byte[] bytes) ExportMarkdown(string id)
        {
            var data = GetData(id);
            var fileName = $"{data.title}.md";
            var contentType = "application/octet-stream";
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    sw.Write(data.content);
                    sw.Close();
                }
                return (fileName, contentType, ms.ToArray());
            }
        }
    }
}
