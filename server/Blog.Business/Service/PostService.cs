using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sixpence.Common;
using Sixpence.Common.IoC;
using Sixpence.ORM.EntityManager;
using Blog.Business.Entity;
using Blog.Business.Model;
using Sixpence.Web.Service;
using Sixpence.Web.Model;
using Sixpence.Web.Entity;
using Blog.Business.Sync.SyncPost;

namespace Blog.Business.Service
{
    public class PostService : EntityService<Post>
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
        public override Post GetData(string id)
        {
            return Manager.ExecuteTransaction(() =>
            {
                var data = base.GetData(id);
                var paramList = new Dictionary<string, object>() { { "@id", id } };
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
            var dataList = Manager.Query<Post>(sql).ToList();

            var categories = dataList
                .GroupBy(p => p.post_type)
                .Select(b => new Model.CategoryModel() { category = b.First().post_type, category_name = b.First().post_type_name, data = new List<CategoryData>() })
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
        public UserInfo GetIndexUser()
        {
            var config = Manager.QueryFirst<Sixpence.Web.Entity.SysConfig>("SELECT * FROM sys_config WHERE code = @code", new Dictionary<string, object>() { { "@code", "index_user" } });
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
