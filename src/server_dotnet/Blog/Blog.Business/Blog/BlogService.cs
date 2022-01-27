using Blog.Business.Upvote;
using Blog.Core;
using Sixpence.ORM.Entity;
using Blog.Core.Module.Role;
using Blog.Core.Profiles;
using Sixpence.Common.Utils;
using Blog.WeChat;
using Blog.WeChat.Material;
using Blog.WeChat.WeChatNews;
using System;
using System.Collections.Generic;

using Sixpence.Common;
using Newtonsoft.Json;
using Blog.Core.Config;
using System.IO;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Module.SysConfig;
using Sixpence.ORM.EntityManager;

namespace Blog.Business.Blog
{
    public class BlogService : EntityService<blog>
    {
        public const string BLOG_SURFACE_NAME = "blog_surface";
        public const string BLOG_CONTENT_NAME = "blog_content";

        #region 构造函数
        public BlogService() : base() { }

        public BlogService(IEntityManager manager) : base(manager) { }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            var sql = $@"
SELECT
	blog.id,
	blog.title,
	blog.blog_type,
	blog.blog_typeName,
	blog.article_typeName,
	blog.created_by,
	blog.created_by_name,
	blog.updated_by,
	blog.updated_by_name,
	blog.created_at,
	blog.updated_at,
	blog.is_series,
	blog.tags,
	COALESCE(blog.reading_times, 0) reading_times,
	COALESCE((SELECT COUNT(1) FROM upvote WHERE objectid = blog.id), 0) upvote_times,
	COALESCE((SELECT COUNT(1) FROM comments WHERE objectid = blog.id), 0) comment_count,
	blog.surfaceid,
	blog.surface_url,
	blog.brief,
	blog.is_pop,
	blog.is_popName
FROM
	blog
WHERE 1=1 AND blog.is_show = 1";
            var orderBy = "blog.is_pop DESC, blog.created_at desc, blog.title, blog.id";
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
            return Manager.ExecuteTransaction(() =>
            {
                var data = base.GetData(id);
                var paramList = new Dictionary<string, object>() { { "@id", id } };
                data.upvote_times = Manager.QueryCount("SELECT COUNT(1) FROM upvote WHERE objectid = @id", paramList);
                data.comment_count = Manager.QueryCount("SELECT COUNT(1) FROM comments WHERE objectid = @id", paramList);
                Manager.Execute("UPDATE blog SET reading_times = COALESCE(reading_times, 0) + 1 WHERE id = @id", paramList);
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
            return Manager.DbClient.Query<string>(sql);
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="blogId"></param>
        public bool Upvote(string blogId)
        {
            var data = Manager.QueryFirst<upvote>("SELECT * FROM upvote WHERE objectid = @id", new Dictionary<string, object>() { { "@id", blogId } });
            if (data != null)
            {
                Manager.Delete(data);
                return false;
            }
            else
            {
                var blog = Manager.QueryFirst<blog>(blogId);
                data = new upvote()
                {
                    id = Guid.NewGuid().ToString(),
                    objectId = blog.id,
                    objectIdName = blog.title,
                    object_ownerid = blog.created_by,
                    object_owneridName = blog.created_by_name,
                    object_type = "blog"
                };
                Manager.Create(data);
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
	to_char(created_at, 'YYYY-MM-DD') AS created_date,
	count(1) AS count
FROM blog
WHERE created_at > to_date(to_char(now(), 'YYYY-01-01'), 'YYYY-MM-DD') AND created_at < to_date(to_char(now(), 'YYYY-12-31'), 'YYYY-MM-DD')
GROUP BY to_char(created_at, 'YYYY-MM-DD')
";
            return Manager.Query<BlogActivityModel>(sql);
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

        /// <summary>
        /// 获取首页用户
        /// </summary>
        /// <returns></returns>
        public user_info GetIndexUser()
        {
            var config = Manager.QueryFirst<sys_config>("SELECT * FROM sys_config WHERE code = @code", new Dictionary<string, object>() { { "@code", "index_user" } });
            if (!string.IsNullOrEmpty(config.value))
            {
                return new UserInfoService(Manager).GetDataByCode(config.value);
            }
            return null;
        }
    }
}
