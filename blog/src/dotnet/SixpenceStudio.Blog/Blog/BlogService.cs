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

        public void UpdateFileObject(string fileId, string objectId)
        {
            var sql = @"
UPDATE sys_file SET objectid = @id WHERE sys_fileid = @fileId;
";
            _cmd.broker.DbClient.Execute(sql, new Dictionary<string, object>() { { "@id", objectId }, { "@fileId", fileId } });
        }

        public override string CreateData(blog t)
        {
            UpdateFileObject(t.imageId, t.Id);
            return base.CreateData(t);
        }

        public override void UpdateData(blog t)
        {
            UpdateFileObject(t.imageId, t.Id);
            base.UpdateData(t);
        }

        public override blog GetData(string id)
        {
            var data = base.GetData(id);
            var sql = @"
SELECT * sys_file WHERE objectid = @id;
";
            var image = _cmd.broker.Retrieve<sys_file>(sql, new Dictionary<string, object>() { { "@id", id } });
            data.imageId = image.sys_fileId;
            data.imageSrc =  "temp\\" + image.name;
            return data;
        }

        public override IList<blog> GetDataList(IList<SearchCondition> searchList)
        {
            var sql = $@"
SELECT
	blog.*,
	sys_file.sys_fileid AS imageId,
	'temp/' || sys_file.name AS imageSrc
FROM
	blog
LEFT JOIN sys_file ON sys_file.objectid = blog.blogid
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

            var data = _cmd.broker.RetrieveMultiple<blog>(sql + where, paramList);
            return data;
        }
    }
}
