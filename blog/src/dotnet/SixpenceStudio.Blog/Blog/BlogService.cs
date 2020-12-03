using SixpenceStudio.Core;
using SixpenceStudio.Core.SysFile;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Utils;
using SixpenceStudio.WeChat;
using SixpenceStudio.WeChat.Material;
using SixpenceStudio.WeChat.WeChatNews;
using System.Collections.Generic;

namespace SixpenceStudio.Blog.Blog
{
    public class BlogService : EntityService<blog>
    {
        public const string BLOG_SURFACE_NAME = "blog_surface";
        public const string BLOG_CONTENT_NAME = "blog_content";

        #region 构造函数
        public BlogService()
        {
            _cmd = new EntityCommand<blog>();
        }

        public BlogService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<blog>(broker);
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
	blog.createdBy,
	blog.createdbyname,
	blog.modifiedBy,
	blog.modifiedbyname,
	blog.createdOn,
	blog.modifiedOn,
	blog.is_series,
	blog.tags,
	COALESCE(blog.reading_times, 0) reading_times,
	COALESCE(blog.upvote_times, 0) upvote_times,
	(SELECT COUNT(1) FROM comments WHERE objectid = blog.blogid) message,
	blog.surfaceid,
	blog.surfacec_url
FROM
	blog
INNER JOIN classification ON classification.code = blog.blog_type AND classification.is_show = 1
WHERE 1=1
";
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = $@"{sql} AND blog.is_series = 0",
                    ViewId = "463BE7FE-5435-4841-A365-C9C946C0D655",
                    CustomFilter = new List<string>() { "title" },
                    Name = "全部博客",
                    OrderBy = "blog.modifiedOn desc, blog.title, blog.blogid"
                },
                new EntityView()
                {
                    Sql = $@"{sql} AND blog.is_series = 1",
                    ViewId = "ACCE50D6-81A5-4240-BD82-126A50764FAB",
                    CustomFilter = new List<string>() { "title" },
                    Name = "全部系列",
                    OrderBy = "blog.modifiedOn desc, blog.title, blog.blogid"
                }
            };
        }

        #region CRUD

        /// <summary>
        /// 删除博客封面
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSurface(string id)
        {
            new SysFileService(Broker).DeleteData(new List<string>() { id });
        }

        /// <summary>
        /// 查询博客
        /// </summary>
        /// <param name="searchList"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        #endregion

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
            return _cmd.Broker.DbClient.Query<string>(sql);
        }

        /// <summary>
        /// 记录阅读次数
        /// </summary>
        /// <param name="blogId">博客 id</param>
        public void RecordReadingTimes(string blogId)
        {
            var sql = @"
UPDATE blog SET reading_times = COALESCE(reading_times, 0) + 1 WHERE blogid = @id
";
            _cmd.Broker.Execute(sql, new Dictionary<string, object>() { { "@id", blogId } });
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="blogId"></param>
        public void Upvote(string blogId)
        {
            var sql = @"
UPDATE blog SET upvote_times = COALESCE(upvote_times, 0) + 1 WHERE blogid = @id
";
            _cmd.Broker.Execute(sql, new Dictionary<string, object>() { { "@id", blogId } });
        }

        /// <summary>
        /// 同步到微信图文素材
        /// </summary>
        /// <param name="id"></param>
        /// <param name="htmlContent"></param>
        public void SyncToWeChat(string id, string htmlContent)
        {
            _cmd.Broker.ExecuteTransaction(() =>
            {
                var data = GetData(id);
                var media = new WeChatMaterialService(_cmd.Broker).CreateData(MaterialType.image, data.surfaceid);
                new WeChatNewsService(_cmd.Broker).CreateData(data.title, media, data.createdByName, "", true, htmlContent, "", true, false);
            });
        }
    }
}
