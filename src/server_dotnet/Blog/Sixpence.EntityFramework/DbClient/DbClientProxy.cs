using Sixpence.Core;
using Sixpence.EntityFramework.Driver;
using Sixpence.Core.Logging;
using Sixpence.Core.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sixpence.EntityFramework.DbClient
{
    /// <summary>
    /// DbClient 代理类（实现日志、本地化Sql转换）
    /// </summary>
    public class DbClientProxy : IDbClient, IDisposable
    {
        #region 构造函数
        public DbClientProxy()
        {
            this.dbClient = new DbClient();
        }

        public DbClientProxy(IDbClient client)
        {
            this.dbClient = client;
        }
        #endregion

        private IDbClient dbClient;

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public IDbConnection DbConnection => dbClient.DbConnection;

        /// <summary>
        /// 数据库链接状态
        /// </summary>
        public ConnectionState ConnectionState => dbClient.ConnectionState;

        public IDbDriver Driver => dbClient.Driver;

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            return dbClient.BeginTransaction();
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            dbClient.Close();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            dbClient.CommitTransaction();
        }

        /// <summary>
        /// 创建临时表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string CreateTemporaryTable(string tableName)
        {
            return dbClient.CreateTemporaryTable(tableName);
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName"></param>
        public void DropTable(string tableName)
        {
            dbClient.DropTable(tableName);
        }

        /// <summary>
        /// 执行SQL，返回受影响行数（记录 log）
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public int Execute(string sqlText, IDictionary<string, object> paramList = null)
        {
            var paramListClone = new Dictionary<string, object>();
            if (paramList != null)
            {
                paramListClone = paramListClone.Concat(paramList).ToDictionary(k => k.Key, v => v.Value);
            }
            var sql = ConvertSqlToDialectSql(sqlText, paramListClone);
            LogUtils.Debug(sql + paramListClone.ToLogString());
            return dbClient.Execute(sql, paramListClone);
        }

        /// <summary>
        /// 执行SQL，返回第一行第一列（记录 log）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, IDictionary<string, object> paramList = null)
        {
            var paramListClone = new Dictionary<string, object>();
            if (paramList != null)
            {
                paramListClone = paramListClone.Concat(paramList).ToDictionary(k => k.Key, v => v.Value);
            }
            sql = ConvertSqlToDialectSql(sql, paramListClone);
            LogUtils.Debug(sql + paramListClone.ToLogString());
            return dbClient.ExecuteScalar(sql, paramListClone);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="connectinString"></param>
        public void Initialize(string connectinString, DriverType driverType)
        {
            dbClient.Initialize(connectinString, driverType);
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        public void Open()
        {
            dbClient.Open();
        }

        /// <summary>
        /// 根据SQL查询，返回传入类型集合（记录 log）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList = null)
        {
            var paramListClone = new Dictionary<string, object>();
            if (paramList != null)
            {
                paramListClone = paramListClone.Concat(paramList).ToDictionary(k => k.Key, v => v.Value);
            }
            sql = ConvertSqlToDialectSql(sql, paramListClone);
            LogUtils.Debug(sql + paramListClone.ToLogString());
            return dbClient.Query<T>(sql, paramListClone);
        }

        /// <summary>
        /// 根据SQL查询，返回DataTable（记录 log）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public DataTable Query(string sql, IDictionary<string, object> paramList = null)
        {
            var paramListClone = new Dictionary<string, object>();
            if (paramList != null)
            {
                paramListClone = paramListClone.Concat(paramList).ToDictionary(k => k.Key, v => v.Value);
            }
            sql = ConvertSqlToDialectSql(sql, paramListClone);
            LogUtils.Debug(sql + paramListClone.ToLogString());
            return dbClient.Query(sql, paramListClone);
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback()
        {
            dbClient.Rollback();
        }


        /// <summary>
        /// 将SQL转换为本地化SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramsList"></param>
        /// <returns></returns>
        public string ConvertSqlToDialectSql(string sql, IDictionary<string, object> paramsList)
        {
            if (paramsList == null || paramsList.Count == 0)
            {
                return sql;
            }
            if (sql.Contains("in@"))
            {
                var toRemovedParamNameList = new Dictionary<string, Dictionary<string, object>>();
                var paramValueNullList = new List<string>(); // 记录传入的InList参数的Value如果为空或者没有值的特殊情况

                foreach (var paramName in paramsList.Keys)
                {
                    if (!paramName.ToLower().StartsWith("in")) continue;
                    var paramValue = paramsList[paramName]?.ToString();
                    if (string.IsNullOrWhiteSpace(paramValue))
                    {
                        paramValueNullList.Add(paramName);
                        continue;
                    }

                    toRemovedParamNameList.Add(paramName, new Dictionary<string, object>());
                    var inListValues = paramValue.Split(',');
                    for (var i = 0; i < inListValues.Length; i++)
                    {
                        toRemovedParamNameList[paramName].Add(paramName.Substring(2, paramName.Length - 2) + i, inListValues[i]);
                    }
                }

                foreach (var paramNameRemoved in toRemovedParamNameList.Keys)
                {
                    paramsList.Remove(paramNameRemoved);
                    foreach (var paramNameAdd in toRemovedParamNameList[paramNameRemoved].Keys)
                    {
                        paramsList.Add(paramNameAdd, toRemovedParamNameList[paramNameRemoved][paramNameAdd]);
                    }

                    var newParamNames = toRemovedParamNameList[paramNameRemoved].Keys.Aggregate((l, n) => l + "," + n);
                    sql = sql.Replace(paramNameRemoved, newParamNames);
                }

                foreach (var paramValueNullName in paramValueNullList)
                {
                    paramsList.Remove(paramValueNullName);
                    sql = sql.Replace(paramValueNullName, "null");
                }

                return sql;
            }
            return sql;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            (dbClient as IDisposable).Dispose();
        }

        /// <summary>
        /// 批量拷贝
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        public void BulkCopy(DataTable dataTable, string tableName)
        {
            dbClient.BulkCopy(dataTable, tableName);
        }
    }
}
