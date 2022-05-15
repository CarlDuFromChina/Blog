using Sixpence.ORM.Entity;
using Sixpence.Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sixpence.ORM.EntityManager;

namespace Blog.Core.Module.VersionScriptExecutionLog
{
    public class VersionScriptExecutionLogService : EntityService<version_script_execution_log>
    {
        #region 构造函数
        public VersionScriptExecutionLogService() : base() { }

        public VersionScriptExecutionLogService(IEntityManager manager) : base(manager) { }
        #endregion

        /// <summary>
        /// 执行SQL脚本并记录（已成功执行过的则跳过）
        /// </summary>
        /// <param name="filePath"></param>
        public int ExecuteScript(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var sql = @"
select * from version_script_execution_log
where name = @name and is_success = 1
";
            var data = Manager.QueryFirst<version_script_execution_log>(sql, new Dictionary<string, object>() { { "@name", fileName } });
            if (data == null)
            {
                data = new version_script_execution_log() { id = Guid.NewGuid().ToString(), name = fileName };
                try
                {
                    Manager.ExecuteSqlScript(filePath);
                    data.is_success = true;
                    Manager.Create(data);
                    return 1;
                }
                catch (Exception ex)
                {
                    data.is_success = false;
                    Manager.Create(data);
                    throw ex;
                }
            }
            return 0;
        }
    }
}
