using Blog.Core.Entity;
using Blog.Core.Module.SysEntity;
using Blog.Core.Module.SysParams;
using Sixpence.Common;
using Sixpence.Common.IoC;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Blog.Core.Module.SysParamGroup
{
    public class SysParamGroupService : EntityService<sys_paramgroup>
    {
        #region 构造函数
        public SysParamGroupService() : base() { }
        public SysParamGroupService(IEntityManager manger) : base(manger) { }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            var sql = @"
SELECT
	*
FROM
	sys_paramgroup
";
            var customFilter = new List<string>() { "name" };
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = sql,
                    CustomFilter = customFilter,
                    OrderBy = "name, created_at",
                    ViewId = "457CA7F7-BE57-4934-9434-3234EAF68E14",
                    Name = "所有的选项集"
                }
            };
        }

        public IEnumerable<SelectOption> GetParams(string code)
        {
            var sql = @"
SELECT 
	sys_param.code AS Value,
	sys_param.name AS Name
FROM sys_param
INNER JOIN sys_paramgroup ON sys_param.sys_paramgroupid = sys_paramgroup.id
WHERE sys_paramgroup.code = @code
";
            return Manager.Query<SelectOption>(sql, new Dictionary<string, object>() { { "@code", code } }).ToList();
        }

        public IEnumerable<IEnumerable<SelectOption>> GetParamsList(string[] paramsList)
        {
            var dataList = new List<List<SelectOption>>();
            return paramsList.Select(item => GetParams(item));
        }

        public IEnumerable<SelectOption> GetEntities(string code)
        {
            var resolve = ServiceContainer.Resolve<IEntityOptionProvider>(name => code.Replace("_", "").ToLower() == name.Replace("EntityOptionProvider", "").ToLower());
            if (resolve != null)
            {
                return resolve.GetOptions();
            }

            var entity = Manager.QueryFirst<sys_entity>(@"select * from sys_entity se where code = @code", new Dictionary<string, object>() { { "@code", code } });
            if (entity != null)
            {
                return Manager.Query<SelectOption>($"select id AS Value, name AS Name from {entity.code}");
            }
            return new List<SelectOption>();
        }

        public IEnumerable<IEnumerable<SelectOption>> GetEntitiyList(string[] paramsList)
        {
            var dataList = new List<List<SelectOption>>();
            return paramsList.Select(item => GetEntities(item));
        }

        public override void DeleteData(List<string> ids)
        {
            Manager.ExecuteTransaction(() =>
            {
                var sql = @"
DELETE FROM sys_param WHERE sys_paramgroupid IN (in@ids)
";
                Manager.Execute(sql, new Dictionary<string, object>() { { "in@ids", ids } });
                base.DeleteData(ids);
            });
        }

        public (string fileName, string ContentType, byte[] bytes) Export(string id)
        {
            var data = GetData(id);
            var paramList = Manager.Query<sys_param>("SELECT * FROM sys_param WHERE sys_paramgroupid = @id", new Dictionary<string, object>() { { "@id", id } });

            var content = $@"INSERT INTO sys_paramgroup (
    sys_paramgroupid,
    name,
    code,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '{data.id}',
    '{data.name}',
    '{data.code}',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT id FROM sys_paramgroup WHERE id = '{data.id}'
);
";
            paramList.Each(param =>
            {
                content += $@"
INSERT INTO sys_param (
    sys_paramid,
    name,
    code,
    sys_paramgroupid,
    sys_paramgroupid_name,
    created_by,
    created_by_name,
    created_at,
    updated_by,
    updated_by_name,
    updated_at
)
SELECT
    '{param.id}',
    '{param.name}',
    '{param.code}',
    '{data.id}',
    '{data.name}',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '{param.id}'
);";
            });

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(ms))
                {
                    writer.WriteLine(content);
                    writer.Close();
                }
                return ($"{data.code}.sql", "application/octet-stream", ms.ToArray());
            }
        }
    }
}