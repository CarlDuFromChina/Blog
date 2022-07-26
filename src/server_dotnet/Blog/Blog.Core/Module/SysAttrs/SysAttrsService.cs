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
using Sixpence.ORM;

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
            var stringType = Manager.Driver.Convert2DbType(typeof(string));
            var dateType = Manager.Driver.Convert2DbType(typeof(DateTime));
            var columns = new List<ColumnOptions>()
            {
                { new ColumnOptions() { Name = "name", LogicalName = "名称", Type = stringType, Length = 100, IsRequire = false } },
                { new ColumnOptions() { Name = "created_by", LogicalName = "创建人", Type = stringType, Length = 40, IsRequire = true } },
                { new ColumnOptions() { Name = "created_by_name", LogicalName = "创建人", Type = stringType, Length = 100, IsRequire = true } },
                { new ColumnOptions() { Name= "created_at", LogicalName = "创建日期", Type = dateType, IsRequire = true } },
                { new ColumnOptions() { Name = "updated_by", LogicalName = "修改人", Type = stringType, Length = 40, IsRequire = true } },
                { new ColumnOptions() { Name = "updated_by_name", LogicalName = "修改人", Type = stringType, Length = 100, IsRequire = true } },
                { new ColumnOptions() { Name = "updated_at", LogicalName = "修改日期", Type = dateType, IsRequire = true } }
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
                    AssertUtil.IsTrue(count > 0, $"实体{entity.code}已存在{item.Name}字段，请勿重复添加");
                    var attrModel = new sys_attrs()
                    {
                        id = Guid.NewGuid().ToString(),
                        code = item.Name,
                        name = item.LogicalName,
                        entityid = entity.id,
                        entityid_name = entity.name,
                        attr_type = item.Type,
                        attr_length = item.Length,
                        isrequire = item.IsRequire == true,
                        default_value = ConvertUtil.ConToString(item.DefaultValue)
                    };
                    Manager.Create(attrModel, false);
                });
            });
        }
    }
}