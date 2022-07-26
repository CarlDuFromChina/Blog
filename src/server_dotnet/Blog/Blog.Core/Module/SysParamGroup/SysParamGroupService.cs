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
    }
}