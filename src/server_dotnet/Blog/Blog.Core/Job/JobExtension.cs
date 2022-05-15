﻿using Blog.Core.Auth;
using Sixpence.ORM.Entity;
using Sixpence.Common.Utils;
using Microsoft.AspNetCore.Builder;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.Common;

using Sixpence.Common.IoC;
using Sixpence.ORM.Driver;
using Sixpence.ORM;

namespace Blog.Core.Job
{
    public static class JobExtension
    {
        public static SelectOption ToSelectOption(this TriggerState triggerState)
        {
            switch (triggerState)
            {
                case TriggerState.Normal:
                    return new SelectOption() { Name = "正常", Value = "0" };
                case TriggerState.Paused:
                    return new SelectOption() { Name = "暂停", Value = "1" };
                case TriggerState.Complete:
                    return new SelectOption() { Name = "完成", Value = "2" };
                case TriggerState.Error:
                    return new SelectOption() { Name = "错误", Value = "3" };
                case TriggerState.Blocked:
                    return new SelectOption() { Name = "阻塞", Value = "4" };
                case TriggerState.None:
                default:
                    return new SelectOption() { Name = "不存在", Value = "-1" };
            }
        }

        public static string GetDelegateType(DriverType dbType)
        {
            switch (dbType)
            {
                case DriverType.Postgresql:
                    return "Quartz.Impl.AdoJobStore.PostgreSQLDelegate, Quartz";
                case DriverType.Mysql:
                    return "Quartz.Impl.AdoJobStore.SQLiteDelegate, Quartz";
                default:
                    return null;
            }
        }

        public static string GetDbBDriver(DriverType dbType)
        {
            switch (dbType)
            {
                case DriverType.Postgresql:
                    return "Npgsql";
                case DriverType.Mysql:
                    return "MySql";
                default:
                    return null;
            }
        }

        public static TriggerState ToTriggerState(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return TriggerState.None;
            }
            return (TriggerState)Convert.ToInt32(value);
        }

        public static IApplicationBuilder UseJob(this IApplicationBuilder app)
        {
            AssemblyUtil
                .GetAssemblies("Blog.*.dll", "Sixpence.*.dll")
                .GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface && type.GetInterfaces().Contains(typeof(IJob)) && !type.IsDefined(typeof(DynamicJobAttribute), true))
                .Each(type => ServiceContainer.Register(typeof(IJob), type));
            JobHelpers.Start();
            return app;
        }
    }
}
