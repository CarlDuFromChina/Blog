using AutoMapper;
using Blog.Core;
using Blog.Core.Auth;
using Blog.Core.Module.SysAttrs;
using Blog.Core.Module.SysEntity;
using Blog.Core.Module.VersionScriptExecutionLog;
using Blog.Core.Utils;
using Sixpence.Common;
using Sixpence.Common.Logging;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using Sixpence.ORM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sixpence.ORM.Entity
{
    public class PostCreateEntities : IPostCreateEntities
    {
        public void Execute(IEntityManager manager, IEnumerable<IEntity> entities)
        {
            var logger = LogFactory.GetLogger("entity");
            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetSystem());
            entities.Each(item =>
                {
                    #region 实体添加自动写入记录
                    var entityName = item.GetEntityName();
                    var entity = manager.QueryFirst<sys_entity>("select * from sys_entity where code = @code", new Dictionary<string, object>() { { "@code", entityName } });
                    if (entity == null)
                    {
                        entity = new sys_entity()
                        {
                            id = Guid.NewGuid().ToString(),
                            name = item.GetLogicalName(),
                            code = item.GetEntityName(),
                            is_sys = item.IsSystemEntity()
                        };
                        manager.Create(entity, false);
                    }
                    #endregion

                    var attrs = item.GetColumns();
                    var attrsList = new SysEntityService(manager).GetEntityAttrs(entity.id).Select(e => e.code);

                    #region 实体字段变更（删除字段）
                    attrsList.Each(attr =>
                    {
                        if (!attrs.Any(item => item.Name.ToLower() == attr.ToLower()))
                        {
                            var sql = @"DELETE FROM sys_attrs WHERE lower(code) = @code AND entityid = @entityid";
                            manager.Execute(sql, new Dictionary<string, object>() { { "@code", attr.ToLower() }, { "@entityid", EntityCache.GetEntity(item.GetEntityName())?.id } });
                            sql = manager.Driver.GetDropColumnSql(item.GetEntityName(), new List<Column>() { new Column() { Name = attr } });
                            manager.Execute(sql);
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
                                id = Guid.NewGuid().ToString(),
                                name = attr.LogicalName,
                                code = attr.Name,
                                entityid = entity.id,
                                entityid_name = entity.name,
                                entityCode = entity.code,
                                attr_type = attr.Type.ToString().ToLower(),
                                attr_length = attr.Length,
                                isrequire = attr.IsRequire.HasValue && attr.IsRequire.Value,
                                default_value = ConvertUtil.ConToString(attr.DefaultValue)
                            };
                            manager.Create(_attr);
                            logger.Debug($"实体{item.GetLogicalName()}（{item.GetEntityName()}）创建字段：{attr.LogicalName}（{attr.Name}）成功");
                        }
                    });
                    #endregion
                });

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
                                var count = new VersionScriptExecutionLogService(manager).ExecuteScript(sqlFile);
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
        }
    }
}
