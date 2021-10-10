using AutoMapper;
using Blog.Core;
using Blog.Core.Auth;
using Blog.Core.Module.SysAttrs;
using Blog.Core.Module.SysEntity;
using Blog.Core.Module.VersionScriptExecutionLog;
using Blog.Core.Utils;
using Microsoft.AspNetCore.Builder;
using Sixpence.Core;
using Sixpence.Core.Logging;
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Entity;
using Sixpence.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sixpence.EntityFramework.Entity
{
    public static class EntityExtension
    {
        public static IApplicationBuilder UseEntityWatcher(this IApplicationBuilder app)
        {
            var logger = LogFactory.GetLogger("entity");
            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetSystem());
            var broker = PersistBrokerFactory.GetPersistBroker();
            var dialect = broker.DbClient.Driver;
            var entityList = ServiceContainer.ResolveAll<IEntity>();
            broker.ExecuteTransaction(() =>
            {
                #region 创建表
                entityList.Each(item =>
                {
                    var entity = broker.Query(dialect.GetTable(item.GetEntityName()));
                    var attrs = item.GetAttrs();
                    if (entity == null || entity.Rows.Count == 0)
                    {
                        var attrSql = attrs
                                .Select(e =>
                                {
                                    return $"{e.Name} {e.Type.GetDescription()}{(e.Length != null ? $"({e.Length.Value})" : "")} {(e.IsRequire.HasValue && e.IsRequire.Value ? "NOT NULL" : "")}{(e.Name == $"{item.GetEntityName()}id" ? " PRIMARY KEY" : "")}";
                                })
                                .Aggregate((a, b) => a + ",\r\n" + b);

                        // 创建表
                        var sql = $@"CREATE TABLE public.{item.GetEntityName()} ({attrSql})";
                        broker.Execute(sql);
                        logger.Info($"实体{item.GetLogicalName()}（{item.GetEntityName()}）创建成功");
                    }
                });
                #endregion

                #region 创建实体记录和实体字段数据
                entityList.Each(item =>
                {
                    #region 实体添加自动写入记录
                    var entityName = item.GetEntityName();
                    var entity = broker.Retrieve<sys_entity>("select * from sys_entity where code = @code", new Dictionary<string, object>() { { "@code", entityName } });
                    if (entity == null)
                    {
                        entity = new sys_entity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            name = item.GetLogicalName(),
                            code = item.GetEntityName(),
                            is_sys = item.IsSystemEntity()
                        };
                        broker.Create(entity, false);
                    }
                    #endregion

                    var attrs = item.GetAttrs();
                    var attrsList = new SysEntityService(broker).GetEntityAttrs(entity.Id).Select(e => e.code);

                    #region 实体字段变更（删除字段）
                    attrsList.Each(attr =>
                    {
                        if (!attrs.Any(item => item.Name.ToLower() == attr.ToLower()))
                        {
                            var sql = @"DELETE FROM sys_attrs WHERE lower(code) = @code AND entityid = @entityid";
                            broker.Execute(sql, new Dictionary<string, object>() { { "@code", attr.ToLower() }, { "@entityid", EntityCache.GetEntity(item.GetEntityName())?.Id } });
                            sql = broker.DbClient.Driver.GetDropColumnSql(item.GetEntityName(), new List<Column>() { new Column() { Name = attr } });
                            broker.Execute(sql);
                            logger.Debug($"实体{item.GetLogicalName()} （{item.GetEntityName()}）删除字段：{attr}");
                        }
                    });
                    #endregion

                    #region 实体字段变更（新增字段）
                    attrs.Each(attr =>
                    {
                        if (!attrsList.Contains(attr.Name))
                        {
                            var _attr = new sys_attrs()
                            {
                                Id = Guid.NewGuid().ToString(),
                                name = attr.LogicalName,
                                code = attr.Name,
                                entityid = entity.Id,
                                entityidname = entity.name,
                                entityCode = entity.code,
                                attr_type = attr.Type.GetDescription(),
                                attr_length = attr.Length,
                                isrequire = attr.IsRequire.HasValue && attr.IsRequire.Value
                            };
                            var columns = new List<Column>() { ServiceContainer.Resolve<IMapper>().Map<Column>(_attr) };
                            var sql = broker.DbClient.Driver.GetAddColumnSql(_attr.entityCode, columns);
                            broker.Create(_attr);
                            broker.Execute(sql);
                            logger.Debug($"实体{item.GetLogicalName()}（{item.GetEntityName()}）创建字段：{attr.LogicalName}（{attr.Name}）成功");
                        }
                    });
                    #endregion

                });

                #endregion

                #region 执行版本更新脚本
                {
                    var vLogger = LogFactory.GetLogger("version");
                    FileHelper.GetFileList("*.sql", FolderType.Version)
                            .OrderBy(item => Path.GetFileName(item))
                            .ToList()
                            .Each(sqlFile =>
                            {
                                try
                                {
                                    var count = new VersionScriptExecutionLogService(broker).ExecuteScript(sqlFile);
                                    if (count == 1)
                                    {
                                        vLogger.Info($"脚本：{Path.GetFileName(sqlFile)}执行成功");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    vLogger.Error($"脚本：{Path.GetFileName(sqlFile)}执行失败", ex);
                                }
                            });
                }
                #endregion
            });

            return app;
        }
    }
}
