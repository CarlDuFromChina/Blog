using Sixpence.EntityFramework.Entity;
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
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Models;
using Blog.Core.Profiles;

namespace Blog.Core.Module.SysEntity
{
    public class SysEntityService : EntityService<sys_entity>
    {
        #region 构造函数
        public SysEntityService()
        {
            _context = new EntityContext<sys_entity>();
        }

        public SysEntityService(IPersistBroker broker)
        {
            _context = new EntityContext<sys_entity>(broker);
        }
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
            return Broker.RetrieveMultiple<sys_attrs>(sql, new Dictionary<string, object>() { { "@id", id } });
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override string CreateData(sys_entity t)
        {
            var id = "";
            Broker.ExecuteTransaction(() =>
            {
                id = base.CreateData(t);
                var sql = Broker.DbClient.Driver.CreateTable(t.code);
                Broker.Execute(sql);
                new SysAttrsService(Broker).AddSystemAttrs(id);
            });
            return id;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="ids"></param>
        public override void DeleteData(List<string> ids)
        {
            Broker.ExecuteTransaction(() =>
            {
                var dataList = Broker.RetrieveMultiple<sys_entity>(ids).ToList();
                base.DeleteData(ids); // 删除实体
                var sql = @"
DELETE FROM sys_attrs WHERE entityid IN (in@ids);
";
                Broker.Execute(sql, new Dictionary<string, object>() { { "in@ids", string.Join(",", ids) } }); // 删除级联字段
                dataList.ForEach(data =>
                {
                    Broker.Execute($"DROP TABLE {data.code}");
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
            var sysEntityAttrList = typeof(SimpleEntity).GetProperties().Select(item => item.Name).ToList();
            foreach (var item in attrs)
            {
                var column = MapperHelper.Map<Column>(item);

                // 实体id和实体name不需要产生
                if (item.code != entity.code + "id" && !sysEntityAttrList.Contains(item.code))
                {
                    var attribute = $@"
        /// <summary>
        /// {column.LogicalName}
        /// </summary>
        [DataMember, Attr(""{column.Name}"", ""{column.LogicalName}"", AttrType.{column.Type}, {column.Length})]
        public {column.Type.ToCSharpType()} {column.Name} {{ get; set; }}
";
                    attributes += attribute;
                }
            }

            var content = $@"
using Sixpence.EntityFramework.Entity;
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
        [Attr(""{entity.code}id"", ""实体id"", DataType.Varchar, 100)]
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