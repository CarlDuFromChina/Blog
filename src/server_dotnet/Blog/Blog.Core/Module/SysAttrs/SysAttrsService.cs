using Sixpence.EntityFramework.Entity;
using Blog.Core.Module.SysEntity;
using Sixpence.Core;
using Sixpence.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Models;
using Blog.Core.Profiles;

namespace Blog.Core.Module.SysAttrs
{
    public class SysAttrsService : EntityService<sys_attrs>
    {
        #region 构造函数
        public SysAttrsService()
        {
            _context = new EntityContext<sys_attrs>();
        }

        public SysAttrsService(IPersistBroker broker)
        {
            _context = new EntityContext<sys_attrs>(broker);
        }
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
            Broker.ExecuteTransaction(() =>
            {
                var entity = Broker.Retrieve<sys_entity>(id);
                columns.ForEach(item =>
                {
                    var sql = @"
SELECT * FROM sys_attrs
WHERE entityid = @id AND code = @code;
";
                    var count = Broker.Query<sys_attrs>(sql, new Dictionary<string, object>() { { "@id", entity.Id }, { "@code", item.Name } }).Count();
                    AssertUtil.CheckBoolean<SpException>(count > 0, $"实体{entity.code}已存在{item.Name}字段，请勿重复添加", "E86150F7-52CC-4FB7-A6C4-B743BF382E92");
                    var attrModel = new sys_attrs()
                    {
                        Id = Guid.NewGuid().ToString(),
                        code = item.Name,
                        name = item.LogicalName,
                        entityid = entity.Id,
                        entityidname = entity.name,
                        attr_type = item.Type.ToString().ToLower(),
                        attr_length = item.Length,
                        isrequire = item.IsRequire == true
                    };
                    Broker.Create(attrModel, false);
                });
                Broker.Execute(Broker.DbClient.Driver.GetAddColumnSql(entity.code, columns));
            });
        }
    }
}