using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blog.Business.Category;
using Blog.Business.Post.Model;
using Blog.Business.Post.Sync;
using Blog.Business.Upvote;
using Sixpence.Web.Auth.UserInfo;
using Sixpence.Web.Module.SysConfig;
using Sixpence.Common;
using Sixpence.Common.IoC;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;

namespace Blog.Business.Post
{
    public class PostService : EntityService<post>
    {
        #region 构造函数
        public PostService() : base() { }

        public PostService(IEntityManager manager) : base(manager) { }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            var sql = $@"
SELECT
	post.id,
	post.title,
	post.post_type,
	post.post_type_name,
	post.article_type_name,
	post.created_by,
	post.created_by_name,
	post.updated_by,
	post.updated_by_name,
	post.created_at,
	post.updated_at,
	post.is_series,
	post.tags,
	COALESCE(post.reading_times, 0) reading_times,
	COALESCE((SELECT COUNT(1) FROM upvote WHERE objectid = post.id), 0) upvote_times,
	COALESCE((SELECT COUNT(1) FROM comments WHERE objectid = post.id), 0) comment_count,
	post.surfaceid,
	post.surface_url,
	post.brief,
	post.is_pop,
	post.is_pop_name
FROM
	post
WHERE 1=1 AND post.is_show = true";
            var orderBy = "post.is_pop DESC, post.created_at desc, post.title, post.id";
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = "SELECT * FROM post",
                    ViewId = "C94EDAAE-0C59-41E6-A373-D4816C2FD882",
                    CustomFilter = new List<string>(){ "title" },
                    Name = "全部博客",
                    OrderBy = orderBy
                },
                new EntityView()
                {
                    Sql = sql + " AND post.is_series is false",
                    ViewId = "463BE7FE-5435-4841-A365-C9C946C0D655",
                    CustomFilter = new List<string>() { "title" },
                    Name = "展示的博客",
                    OrderBy = orderBy
                },
                new EntityView()
                {
                    Sql = sql + " AND post.is_series is true",
                    ViewId = "834F8083-47BC-42F3-A6B2-DE25BE755714",
                    CustomFilter = new List<string>() { "title" },
                    Name = "展示的系列",
                    OrderBy =orderBy
                },
                new EntityView()
                {
                    Sql = $@"SELECT * FROM post WHERE post.is_series is true",
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
        public override post GetData(string id)
        {
            return Manager.ExecuteTransaction(() =>
            {
                var data = base.GetData(id);
                var paramList = new Dictionary<string, object>() { { "@id", id } };
                data.upvote_times = Manager.QueryCount("SELECT COUNT(1) FROM upvote WHERE objectid = @id", paramList);
                data.comment_count = Manager.QueryCount("SELECT COUNT(1) FROM comments WHERE objectid = @id", paramList);
                Manager.Execute("UPDATE post SET reading_times = COALESCE(reading_times, 0) + 1 WHERE id = @id", paramList);
                return data;
            });
        }

        public PostCategories GetCategories()
        {
            var data = new PostCategories();

            var sql = @"
SELECT
	post.id,
	post.title,
	post.post_type,
	post.post_type_name
FROM
	post
WHERE 1=1 AND post.is_show is true";
            var dataList = Manager.Query<post>(sql).ToList();

            var categories = dataList
                .GroupBy(p => p.post_type)
                .Select(b => new Model.Category() { category = b.First().post_type, category_name = b.First().post_type_name, data = new List<CategoryData>() })
                .ToList();

            dataList.Each(item =>
            {
                var category = categories.Where(d => d.category.Equals(item.post_type)).FirstOrDefault();
                category.data.Add(new CategoryData() { id = item.id, title = item.title });
            });

            return new PostCategories()
            {
                count = categories.Count(),
                data = categories
            };
        }

        /// <summary>
        /// 获取博客路由
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetPostRouters()
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
        /// <param name="postid"></param>
        public bool Upvote(string postid)
        {
            var data = Manager.QueryFirst<upvote>("SELECT * FROM upvote WHERE objectid = @id", new Dictionary<string, object>() { { "@id", postid } });
            if (data != null)
            {
                Manager.Delete(data);
                return false;
            }
            else
            {
                var post = Manager.QueryFirst<post>(postid);
                data = new upvote()
                {
                    id = Guid.NewGuid().ToString(),
                    objectId = post.id,
                    objectid_name = post.title,
                    object_ownerid = post.created_by,
                    object_ownerid_name = post.created_by_name,
                    object_type = "post"
                };
                Manager.Create(data);
                return true;
            }
        }

        /// <summary>
        /// 获取创作记录日历
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PostActivityModel> GetActivity()
        {
            var sql = @"
SELECT
	to_char(created_at, 'YYYY-MM-DD') AS created_date,
	count(1) AS count
FROM post
WHERE created_at > to_date(to_char(now(), 'YYYY-01-01'), 'YYYY-MM-DD') AND created_at < to_date(to_char(now(), 'YYYY-12-31'), 'YYYY-MM-DD')
GROUP BY to_char(created_at, 'YYYY-MM-DD')
";
            return Manager.Query<PostActivityModel>(sql);
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

        /// <summary>
        /// 同步博客到第三方系统
        /// </summary>
        /// <param name="id"></param>
        /// <param name="destination"></param>
        /// <param name="param"></param>
        public void SyncPost(string id, string destination, object param)
        {
            ServiceContainer.Resolve<ISyncPost>((name) => name.Contains(destination, StringComparison.OrdinalIgnoreCase))?.Execute(id, param);
        }
    }
}
