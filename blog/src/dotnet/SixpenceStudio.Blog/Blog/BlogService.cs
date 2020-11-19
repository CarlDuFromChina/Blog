using SixpenceStudio.BaseSite.SysFile;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.Utils;
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

        public override IList<EntityView<blog>> GetViewList()
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
	sys_file.sys_fileid AS imageId
FROM
	blog
LEFT JOIN sys_file ON sys_file.objectid = blog.blogid AND sys_file.file_type = '{BLOG_SURFACE_NAME}'
INNER JOIN classification ON classification.code = blog.blog_type AND classification.is_show = 1
WHERE 1=1
";
            return new List<EntityView<blog>>()
            {
                new EntityView<blog>()
                {
                    Sql = $@"{sql} AND blog.is_series = 0",
                    ViewId = "463BE7FE-5435-4841-A365-C9C946C0D655",
                    CustomFilter = new List<string>() { "title" },
                    Name = "全部博客",
                    OrderBy = "blog.modifiedOn desc, blog.title, blog.blogid"
                },
                new EntityView<blog>()
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
        public override void DeleteData(List<string> ids)
        {
            _cmd.broker.ExecuteTransaction(() =>
            {
                ids.ForEach(item => DeleteBlogImages(item));
                base.DeleteData(ids);
            });
        }

        /// <summary>
        /// 删除博客封面
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSurface(string id)
        {
            new SysFileService().DeleteData(new List<string>() { id });
        }

        /// <summary>
        /// 删除博客所有图片
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBlogImages(string id)
        {
            var sql = @"
DELETE from sys_file WHERE objectid = @id;
";
            _cmd.broker.DbClient.Execute(sql, new Dictionary<string, object>() { { "@id", id } });
        }

        /// <summary>
        /// 获取博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override blog GetData(string id)
        {
            var data = base.GetData(id);
            var sql = $@"
SELECT * FROM sys_file WHERE objectid = @id and file_type = '{BLOG_SURFACE_NAME}';
";
            // 获取博客封面
            var image = _cmd.broker.Retrieve<sys_file>(sql, new Dictionary<string, object>() { { "@id", id } });
            data.imageId = image?.Id;
            return data;
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
            return _cmd.broker.DbClient.Query<string>(sql);
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
            _cmd.broker.Execute(sql, new Dictionary<string, object>() { { "@id", blogId } });
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
            _cmd.broker.Execute(sql, new Dictionary<string, object>() { { "@id", blogId } });
        }

        /// <summary>
        /// 同步到微信图文素材
        /// </summary>
        /// <param name="id"></param>
        /// <param name="htmlContent"></param>
        public void SyncToWeChat(string id, string htmlContent)
        {
            _cmd.broker.ExecuteTransaction(() =>
            {
                var data = GetData(id);
                var media = new WeChatMaterialService().AddMaterial(WeChatMaterialExtension.MaterialType.image, data.imageId);
                new WeChatNewsService(Broker).AddNews(data.title, media, data.createdByName, "", true, htmlContent, "", true, false);
            });
        }
    }
}
