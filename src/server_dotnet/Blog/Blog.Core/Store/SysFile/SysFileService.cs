using Blog.Core.Config;
using Blog.Core.Data;
using System.Collections.Generic;

namespace Blog.Core.Store.SysFile
{
    public class SysFileService : EntityService<sys_file>
    {
        #region 构造函数
        public SysFileService()
        {
            this._context = new EntityContext<sys_file>();
        }

        public SysFileService(IPersistBroker broker)
        {
            this._context = new EntityContext<sys_file>(broker);
        }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Name = "所有文件",
                    ViewId = "DD1D72FB-D7DE-49AC-B387-273375E6A7BA",
                    Sql = @"
SELECT
	sys_fileid,
	NAME,
	content_type,
	createdon,
	createdbyname
FROM
	sys_file
",
                    OrderBy = "createdon desc",
                    CustomFilter = new List<string>(){ "name" }
                },
                new EntityView()
                {
                    Name = "图库",
                    ViewId = "3BCF6C07-2B49-4D69-9EB1-A3D5B721C976",
                    Sql = @"
SELECT
	sys_fileid,
	NAME,
	createdon,
	createdbyname,
	concat('/api/SysFile/Download?objectId=', sys_fileid) AS downloadUrl
FROM
	sys_file
",
                    OrderBy = "",
                    CustomFilter = new List<string>(){ "name" }
                }
            };
        }

        public IList<sys_file> GetDattaByCode(string code)
        {
            var sql = @"
SELECT * FROM sys_file
WHERE hash_code = @code
";
            return Broker.RetrieveMultiple<sys_file>(sql, new Dictionary<string, object>() { { "@code", code } });
        }
    }
}