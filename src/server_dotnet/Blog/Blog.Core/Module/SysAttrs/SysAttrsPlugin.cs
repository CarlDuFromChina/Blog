﻿using Blog.Core.Module.SysEntity;
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
            var entity = context.Entity;
            var broker = context.Broker;

            switch (context.Action)
            {
                case EntityAction.PreCreate:
                    {
                        var data = entity as sys_attrs;
                        var columns = new List<Column>() { MapperHelper.Map<Column>(data) };
                        broker.Execute(broker.DbClient.Driver.GetAddColumnSql(data.entityCode, columns));
                    }
                    break;
                case EntityAction.PostDelete:
                    {
                        var column = new Column() { Name = entity.GetAttributeValue<string>("code") };
                        var tableName = broker.Retrieve<sys_entity>(entity.GetAttributeValue<string>("entityid"))?.code;
                        if (!string.IsNullOrEmpty(tableName))
                        {
                            broker.Execute(broker.DbClient.Driver.GetDropColumnSql(tableName, new List<Column>() { column }));
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
