using Sixpence.EntityFramework.Entity;
using Blog.Core.Module.SysEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sixpence.EntityFramework.SelectOption;
using Sixpence.EntityFramework.Broker;
using Sixpence.Common;
using System.IO;
using Blog.Core.Module.SysParams;
using Sixpence.Common.IoC;

namespace Blog.Core.Module.SysParamGroup
{
    public class SysParamGroupService : EntityService<sys_paramgroup>
    {
        #region 构造函数
        public SysParamGroupService()
        {
            this._context = new EntityContext<sys_paramgroup>();
        }
        public SysParamGroupService(IPersistBroker broker)
        {
            this._context = new EntityContext<sys_paramgroup>(broker);
        }
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
                    OrderBy = "name, createdon",
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
INNER JOIN sys_paramgroup ON sys_param.sys_paramgroupid = sys_paramgroup.sys_paramgroupid
WHERE sys_paramgroup.code = @code
";
            return Broker.Query<SelectOption>(sql, new Dictionary<string, object>() { { "@code", code } }).ToList();
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

            var entity = Broker.Retrieve<sys_entity>(@"select * from sys_entity se where code = @code", new Dictionary<string, object>() { { "@code", code } });
            if (entity != null)
            {
                return Broker.Query<SelectOption>($"select {entity.code}id AS Value, name AS Name from {entity.code}");
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
            Broker.ExecuteTransaction(() =>
            {
                var sql = @"
DELETE FROM sys_param WHERE sys_paramgroupid IN (in@ids)
";
                Broker.Execute(sql, new Dictionary<string, object>() { { "in@ids", ids } });
                base.DeleteData(ids);
            });
        }

        public (string fileName, string ContentType, byte[] bytes) Export(string id)
        {
            var data = GetData(id);
            var paramList = Broker.RetrieveMultiple<sys_param>("SELECT * FROM sys_param WHERE sys_paramgroupid = @id", new Dictionary<string, object>() { { "@id", id } });

            var content = $@"INSERT INTO sys_paramgroup (
    sys_paramgroupid,
    name,
    code,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
)
SELECT
    '{data.Id}',
    '{data.name}',
    '{data.code}',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramgroupid FROM sys_paramgroup WHERE sys_paramgroupid = '{data.Id}'
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
    sys_paramgroupidname,
    createdby,
    createdbyname,
    createdon,
    modifiedby,
    modifiedbyname,
    modifiedon
)
SELECT
    '{param.Id}',
    '{param.name}',
    '{param.code}',
    '{data.Id}',
    '{data.name}',
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW()),
    '111111111-11111-1111-1111-111111111111',
    '系统',
    (SELECT NOW())
WHERE NOT EXISTS (
    SELECT sys_paramid FROM sys_param WHERE sys_paramid = '{param.Id}'
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