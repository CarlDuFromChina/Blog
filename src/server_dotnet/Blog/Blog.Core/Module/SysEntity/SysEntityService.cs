using Sixpence.ORM.Entity;
using Blog.Core.Module.SysAttrs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Web;
using Sixpence.ORM.Models;
using Blog.Core.Profiles;
using Sixpence.ORM.EntityManager;

namespace Blog.Core.Module.SysEntity
{
    public class SysEntityService : EntityService<sys_entity>
    {
        #region 构造函数
        public SysEntityService() : base() { }

        public SysEntityService(IEntityManager manager) : base(manager) { }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            var sql = @"
SELECT
	*
FROM
	sys_entity
";
            var customFilter = new List<string>() { "name" };
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = sql,
                    CustomFilter = customFilter,
                    OrderBy = "name, createdon",
                    ViewId = "FBEC5163-587B-437E-995F-1DC97229C906",
                    Name = "所有的实体"
                }
            };
        }

        /// <summary>
        /// 根据实体 id 查询字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<sys_attrs> GetEntityAttrs(string id)
        {
            var sql = @"
SELECT
	*
FROM 
	sys_attrs
WHERE
	entityid = @id
";
            return Manager.Query<sys_attrs>(sql, new Dictionary<string, object>() { { "@id", id } }).ToList();
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override string CreateData(sys_entity t)
        {
            var id = "";
            Manager.ExecuteTransaction(() =>
            {
                id = base.CreateData(t);
                var sql = $"CREATE TABLE {t.code} (id VARCHAR(100) PRIMARY KEY)";
                Manager.Execute(sql);
                new SysAttrsService(Manager).AddSystemAttrs(id);
            });
            return id;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="ids"></param>
        public override void DeleteData(List<string> ids)
        {
            Manager.ExecuteTransaction(() =>
            {
                var dataList = Manager.Query<sys_entity>(ids).ToList();
                base.DeleteData(ids); // 删除实体
                var sql = @"
DELETE FROM sys_attrs WHERE entityid IN (in@ids);
";
                Manager.Execute(sql, new Dictionary<string, object>() { { "in@ids", string.Join(",", ids) } }); // 删除级联字段
                dataList.ForEach(data =>
                {
                    Manager.Execute($"DROP TABLE {data.code}");
                });
            });
        }

        /// <summary>
        /// 导出实体类
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public (string fileName, string ContentType, byte[] bytes) Export(string entityId)
        {
            var attrs = GetEntityAttrs(entityId);
            var entity = GetData(entityId);
            
            var attributes = "";
            foreach (var item in attrs)
            {
                var column = MapperHelper.Map<Column>(item);

                // 实体id和实体name不需要产生
                if (item.code != entity.code + "id")
                {
                    var attribute = $@"
        /// <summary>
        /// {column.LogicalName}
        /// </summary>
        [DataMember, Column(""{column.Name}"", ""{column.LogicalName}"", AttrType.{column.Type}, {column.Length})]
        public {column.Type.ToCSharpType()} {column.Name} {{ get; set; }}
";
                    attributes += attribute;
                }
            }

            var content = $@"
using Sixpence.ORM.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.Core.SysEntity
{{
    [Entity(""{entity.code}"",""{entity.GetLogicalName()}"", {entity.is_sys})]
    public partial class {entity.code} : BaseEntity
    {{
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Column(""{entity.code}id"", ""实体id"", DataType.Varchar, 100)]
        public string {entity.code}Id
        {{
            get
            {{
                return this.Id;
            }}
            set
            {{
                this.Id = value;
            }}
        }}

        {attributes}
    }}
}}
";

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(ms))
                {
                    writer.WriteLine(content);
                    writer.Close();
                }
                return ($"{entity.code}.sql", "application/octet-stream", ms.ToArray());
            }
        }
    }
}