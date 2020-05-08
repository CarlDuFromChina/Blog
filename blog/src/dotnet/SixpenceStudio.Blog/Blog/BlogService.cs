﻿using SixpenceStudio.BaseSite.SysFile;
using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.Service;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region CRUD
        public override void DeleteData(List<string> ids)
        {
            ids.ForEach(item => DeleteBlogImages(item));
            base.DeleteData(ids);
        }

        /// <summary>
        /// 更新博客封面图片
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="fileId"></param>
        public void UpdateSurface(string objectId, string fileId)
        {
            var sql = @"
UPDATE sys_file SET objectid = @objectid WHERE sys_fileid = @id
";
            _cmd.broker.DbClient.Execute(sql, new Dictionary<string, object>() { { "@objectid", objectId }, { "@id", fileId } });
        }

        public override string CreateData(blog t)
        {
            UpdateSurface(t.Id, t.imageId);
            return base.CreateData(t);
        }

        public override void UpdateData(blog t)
        {
            UpdateSurface(t.Id, t.imageId);
            base.UpdateData(t);
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
            data.imageSrc = $"{FileUtils.FILE_FOLDER}/" + image?.name;
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
        public override DataModel<blog> GetDataList(IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex)
        {
            var sql = $@"
SELECT
	blog.*,
	sys_file.sys_fileid AS imageId,
	'{FileUtils.FILE_FOLDER}/' || sys_file.name AS imageSrc
FROM
	blog
LEFT JOIN sys_file ON sys_file.objectid = blog.blogid AND sys_file.file_type = '{BLOG_SURFACE_NAME}'
WHERE 1=1
";
            var where = string.Empty;
            var paramList = new Dictionary<string, object>();

            if (searchList != null)
            {
                var count = 0;
                foreach (var search in searchList)
                {
                    where += $" AND blog.{search.Name} = @params{count}";
                    paramList.Add($"@params{count++}", search.Value);
                }
            }

            var data = _cmd.broker.RetrieveMultiple<blog>(sql + where + " ORDER BY blog.createdon desc, blog.blogid", paramList, orderBy, pageSize, pageIndex, out var recordCount);
            return new DataModel<blog>()
            {
                DataList = data,
                RecordCount = recordCount
            };
        }
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
    }
}
