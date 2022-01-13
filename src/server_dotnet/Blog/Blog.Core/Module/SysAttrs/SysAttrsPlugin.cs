using Blog.Core.Module.SysEntity;
using Blog.Core.Profiles;
using Sixpence.Common;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using Sixpence.ORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.SysAttrs
{
    public class SysAttrsPlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            var entity = context.Entity;
            var manager = context.EntityManager;

            switch (context.Action)
            {
                case EntityAction.PreCreate:
                    {
                        var data = entity as sys_attrs;
                        var columns = new List<Column>() { MapperHelper.Map<Column>(data) };
                        manager.Execute(manager.DbClient.Driver.GetAddColumnSql(data.entityCode, columns));
                    }
                    break;
                case EntityAction.PostDelete:
                    {
                        var column = new Column() { Name = entity.GetAttributeValue<string>("code") };
                        var tableName = manager.QueryFirst<sys_entity>(entity.GetAttributeValue<string>("entityid"))?.code;
                        if (!string.IsNullOrEmpty(tableName))
                        {
                            manager.Execute(manager.DbClient.Driver.GetDropColumnSql(tableName, new List<Column>() { column }));
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
