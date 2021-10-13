using Blog.Core.Module.SysEntity;
using Blog.Core.Profiles;
using Sixpence.Core;
using Sixpence.Core.Utils;
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Module.SysAttrs
{
    public class SysAttrsPlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    {
                        var data = context.Entity as sys_attrs;
                        var columns = new List<Column>() { MapperHelper.Map<Column>(data) };
                        var sql = context.Broker.DbClient.Driver.GetAddColumnSql(data.entityCode, columns);
                        context.Broker.Execute(sql);
                    }
                    break;
                case EntityAction.PostDelete:
                    {
                        var column = new Column() { Name = context.Entity.GetAttributeValue<string>("code") };
                        var tableName = context.Broker.Retrieve<sys_entity>(context.Entity.GetAttributeValue<string>("entityid"))?.code;
                        if (!string.IsNullOrEmpty(tableName))
                        {
                            var sql = context.Broker.DbClient.Driver.GetDropColumnSql(tableName, new List<Column>() { column });
                            context.Broker.Execute(sql);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
