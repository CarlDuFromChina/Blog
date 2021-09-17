using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository;
using Sixpence.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.Core.Logging
{
    /// <summary>
    /// 日志工厂类
    /// </summary>
    public class LogFactory
    {
        private static readonly Object lockObject = new object();

        static LogFactory()
        {
            ILoggerRepository repository = LoggerManager.CreateRepository("SixpenceCoreLogging");
            XmlConfigurator.Configure(repository, new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILog GetLogger(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            if (MemoryCacheUtil.Contains(name))
            {
                return MemoryCacheUtil.GetCacheItem<ILog>(name);
            }
            else
            {
                lock (lockObject)
                {
                    if (MemoryCacheUtil.Contains(name))
                    {
                        return MemoryCacheUtil.GetCacheItem<ILog>(name);
                    }
                    var logger = CreateLoggerInstance(name);
                    var now = DateTime.Now.AddDays(1);
                    var expireDate = new DateTime(now.Year, now.Month, now.Day); // 晚上0点过期
                    MemoryCacheUtil.Set(name, logger, expireDate);
                    return logger;
                }
            }
        }

        /// <summary>
        /// 获取日志实例
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        public static ILog GetLogger(LogType logType = LogType.All)
        {
            switch (logType)
            {
                case LogType.Error:
                    return LogManager.GetLogger("Error");
                case LogType.All:
                default:
                    return LogManager.GetLogger("All");
            }
        }

        /// <summary>
        /// 创建日志实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static ILog CreateLoggerInstance(string name)
        {
            // Pattern Layout
            PatternLayout layout = new PatternLayout("[%logger][%date]%message\r\n");
            // Level Filter
            LevelMatchFilter filter = new LevelMatchFilter();
            filter.LevelToMatch = Level.All;
            filter.ActivateOptions();
            // File Appender
            RollingFileAppender appender = new RollingFileAppender();
            // 目录
            appender.File = $"log{Path.AltDirectorySeparatorChar}{name}.log";
            // 立即写入磁盘
            appender.ImmediateFlush = true;
            // true：追加到文件；false：覆盖文件
            appender.AppendToFile = true;
            // 新的日期或者文件大小达到上限，新建一个文件
            appender.RollingStyle = RollingFileAppender.RollingMode.Once;
            // 文件大小达到上限，新建文件时，文件编号放到文件后缀前面
            appender.PreserveLogFileNameExtension = true;
            // 最小锁定模型以允许多个进程可以写入同一个文件
            appender.LockingModel = new FileAppender.MinimalLock();
            appender.Name = $"{name}Appender";
            appender.AddFilter(filter);
            appender.Layout = layout;
            appender.ActivateOptions();
            // 设置无限备份=-1 ，最大备份数为30
            appender.MaxSizeRollBackups = 30;
            appender.StaticLogFileName = true;
            string repositoryName = $"{name}Repository";
            try
            {
                LoggerManager.GetRepository(repositoryName);
            }
            catch
            {
                ILoggerRepository repository = LoggerManager.CreateRepository(repositoryName);
                BasicConfigurator.Configure(repository, appender);
            }
            var logger = LogManager.GetLogger(repositoryName, name);
            return logger;
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        [Description("报错信息")]
        Error,
        [Description("信息")]
        All
    }
}
