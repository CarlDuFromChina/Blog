using Sixpence.ORM.Entity;
using Blog.Core.Job;
using Blog.Core.Module.SysConfig;
using Blog.Core.Store.SysFile;
using Sixpence.Common.Utils;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sixpence.Common;
using Blog.Core.Utils;

namespace Blog.Core.Module.Job
{
    /// <summary>
    /// 系统作业（Daily）
    /// </summary>
    public class SystemJob : JobBase
    {
        public override string Name => "系统作业";

        public override IScheduleBuilder ScheduleBuilder => CronScheduleBuilder.CronSchedule("0 0 0 * * ?");

        public override string Description => "清理系统无效文件及整理日志";

        public override void Executing(IJobExecutionContext context)
        {
            ArchiveLog();
            DeleteTempFiles();
        }

        /// <summary>
        /// 归档日志
        /// </summary>
        private void ArchiveLog()
        {
            try
            {
                var fileList = FileHelper.GetFileList("*.log", FolderType.Log, SearchOption.TopDirectoryOnly);
                var targetPath = FolderType.LogArchive.GetPath();
                fileList.Each(item =>
                {
                    if (File.Exists(item))
                    {
                        var file = new FileInfo(item);
                        File.Move(item, Path.Combine(targetPath, DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + " " + file.Name));
                    }
                });
                DeleteLog();
            }
            catch (Exception e)
            {
                Logger.Error("日志归档出现异常", e);
                throw e;
            }
        }

        /// <summary>
        /// 删除归档里的log
        /// </summary>
        private void DeleteLog()
        {
            var days = SysConfigFactory.GetValue<BackupLogSysConfig>();
            var files = FileHelper.GetFileList("*.log", FolderType.LogArchive);
            var logNameList = new List<string>();

            // 需要保留的log
            for (int i = 0; i < Convert.ToInt32(days); i++)
            {
                logNameList.Add(DateTime.Now.AddDays(-i).ToString("yyyyMMdd"));
            }

            // 删除不需要保留的log
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (logNameList.Count(Item => Path.GetFileName(file).Contains(Item)) == 0)
                {
                    FileUtil.DeleteFile(file);
                }
            }
        }

        /// <summary>
        /// 删除临时文件
        /// </summary>
        private void DeleteTempFiles()
        {
            var filePath = FolderType.Temp.GetPath();
            Logger.Info($"开始删除[{filePath}]下所有文件");
            try
            {
                FileUtil.DeleteFolder(filePath);
                Logger.Info("删除成功");
            }
            catch (Exception ex)
            {
                Logger.Error("删除失败", ex);
                throw ex;
            }
        }
    }
}