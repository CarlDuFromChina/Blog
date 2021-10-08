using Sixpence.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sixpence.EntityFramework.Broker
{
    public static class IPersistBrokerExecuteExtension
    {
        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        public static int Execute(this IPersistBroker broker, string sql, IDictionary<string, object> paramList = null)
        {
            return broker.DbClient.Execute(sql, paramList);
        }

        /// <summary>
        /// 执行Sql返回第一行第一列记录
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this IPersistBroker broker, string sql, IDictionary<string, object> paramList = null)
        {
            return broker.DbClient.ExecuteScalar(sql, paramList);
        }

        /// <summary>
        /// 执行SQL文件
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="sqlFile"></param>
        /// <returns></returns>
        public static int ExecuteSqlScript(this IPersistBroker broker, string sqlFile)
        {
            int returnValue = -1;
            int sqlCount = 0, errorCount = 0;
            if (!File.Exists(sqlFile))
            {
                LogUtils.Error($"文件({sqlFile})不存在");
                return -1;
            }
            using (StreamReader sr = new StreamReader(sqlFile))
            {
                string line = string.Empty;
                char spaceChar = ' ';
                string newLIne = "\r\n", semicolon = ";";
                string sprit = "/", whiffletree = "-";
                string sql = string.Empty;
                do
                {
                    line = sr.ReadLine();
                    // 文件结束
                    if (line == null) break;
                    // 跳过注释行
                    if (line.StartsWith(sprit) || line.StartsWith(whiffletree)) continue;
                    // 去除右边空格
                    line = line.TrimEnd(spaceChar);
                    sql += line;
                    // 以分号(;)结尾，则执行SQL
                    if (sql.EndsWith(semicolon))
                    {
                        try
                        {
                            sqlCount++;
                            broker.Execute(sql, null);
                        }
                        catch (Exception ex)
                        {
                            errorCount++;
                            LogUtils.Error(sql + newLIne + ex.Message, ex);
                        }
                        sql = string.Empty;
                    }
                    else
                    {
                        // 添加换行符
                        if (sql.Length > 0) sql += newLIne;
                    }
                } while (true);
            }
            if (sqlCount > 0 && errorCount == 0)
                returnValue = 1;
            if (sqlCount == 0 && errorCount == 0)
                returnValue = 0;
            else if (sqlCount > errorCount && errorCount > 0)
                returnValue = -1;
            else if (sqlCount == errorCount)
                returnValue = -2;
            return returnValue;
        }
    }
}
