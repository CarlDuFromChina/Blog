using Sixpence.ORM.Entity;
using Blog.Core.Module.SysEntity;
using Sixpence.Common;
using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sixpence.ORM.Models;
using Blog.Core.Profiles;
using Sixpence.ORM.EntityManager;

namespace Blog.Core.Module.SysAttrs
{
    public class SysAttrsService : EntityService<sys_attrs>
    {
        #region 构造函数
        public SysAttrsService() : base() { }

        public SysAttrsService(IEntityManager manager) : base(manager) { }
        #endregion

        /// <summary>
        /// 添加系统字段
        /// </summary>
        /// <param name="id"></param>
        public void AddSystemAttrs(string id)
        {
            var columns = new List<Column>()
            {
                { new Column() { Name = "name", LogicalName = "名称", Type = DataType.Varchar, Length = 100, IsRequire = false } },
                { new Column() { Name = "createdBy", LogicalName = "创建人", Type = DataType.Varchar, Length = 40, IsRequire = true } },
                { new Column() { Name = "createdByName", LogicalName = "创建人", Type = DataType.Varchar, Length = 100, IsRequire = true } },
                { new Column() { Name= "createdOn", LogicalName = "创建日期", Type = DataType.Timestamp, IsRequire = true } },
                { new Column() { Name = "modifiedBy", LogicalName = "修改人", Type = DataType.Varchar, Length = 40, IsRequire = true } },
                { new Column() { Name = "modifiedByName", LogicalName = "修改人", Type = DataType.Varchar, Length = 100, IsRequire = true } },
                { new Column() { Name = "modifiedOn", LogicalName = "修改日期", Type = DataType.Timestamp, IsRequire = true } }
            };
            Manager.ExecuteTransaction(() =>
            {
                var entity = Manager.QueryFirst<sys_entity>(id);
                columns.ForEach(item =>
                {
                    var sql = @"
SELECT * FROM sys_attrs
WHERE entityid = @id AND code = @code;
";
                    var count = Manager.Query<sys_attrs>(sql, new Dictionary<string, object>() { { "@id", entity.id }, { "@code", item.Name } }).Count();
                    AssertUtil.CheckBoolean<SpException>(count > 0, $"实体{entity.code}已存在{item.Name}字段，请勿重复添加", "E86150F7-52CC-4FB7-A6C4-B743BF382E92");
                    var attrModel = new sys_attrs()
                    {
                        id = Guid.NewGuid().ToString(),
                        code = item.Name,
                        name = item.LogicalName,
                        entityid = entity.id,
                        entityidname = entity.name,
                        attr_type = item.Type.ToString().ToLower(),
                        attr_length = item.Length,
                        isrequire = item.IsRequire == true,
                        default_value = ConvertUtil.ConToString(item.DefaultValue)
                    };
                    Manager.Create(attrModel, false);
                });
                Manager.Execute(Manager.Driver.GetAddColumnSql(entity.code, columns));
            });
        }
    }
}