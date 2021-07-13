using Blog.Core.Auth;
using Blog.Core.Data;
using Blog.Core.Utils;
using Microsoft.AspNetCore.Builder;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .GetAssemblies()
                .GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface && type.GetInterfaces().Contains(typeof(IJob)) && !type.IsDefined(typeof(DynamicJobAttribute), true))
                .Each(type => ServiceContainer.Register(typeof(IJob), type));
            JobHelpers.Start();
            return app;
        }
    }
}
