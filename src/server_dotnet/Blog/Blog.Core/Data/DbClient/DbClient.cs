using Blog.Core.Data.Driver;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Data.DbClient
{
    /// <summary>
    /// 数据库实例
    /// </summary>
    internal sealed class DbClient : IDbClient, IDisposable
    {

        /// <summary>
        /// 数据库连接实例
        /// </summary>
        public IDbConnection DbConnection { get; private set; }

        private IDbDriver driver;
        public IDbDriver Driver => driver;

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        public void Initialize(string connectionString, DriverType driverType)
        {
            driver = ServiceContainer.Resolve<IDbDriver>($"{driverType}Driver");
            DbConnection = driver.GetDbConnection(connectionString);
        }

        /// <summary>
        ///获取数据库连接状态 
        /// </summary>
        /// <returns></returns>
        public ConnectionState ConnectionState => DbConnection.State;

        #region 开启数据库连接
        //数据库打开、关闭的计数器
        private int _dbOpenCounter;

        /// <summary>
        ///打开数据库的连接（如果已经Open，则忽略）
        /// </summary>
        public void Open()
        {
            //counter = 0代表没有打开过，否则说明已经打开过了，不需要再打开
            if (_dbOpenCounter++ == 0)
            {
                if (DbConnection.State != ConnectionState.Open)
                    DbConnection.Open();
            }

        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            //counter先自减1，然后判断是否=0，是的话代表是最后一次关闭
            if (--_dbOpenCounter == 0)
            {
                if (DbConnection.State != ConnectionState.Closed)
                {
                    DbConnection?.Close();
                }
            }
        }
        #endregion

        #region 事务

        private IDbTransaction _trans;
        private int _transCounter;

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            if (_transCounter++ == 0)
            {
                _trans = DbConnection.BeginTransaction();
            }
            return _trans;
        }

        /// <summary>
        /// 提交数据库的事务
        /// </summary>
        public void CommitTransaction()
        {
            if (--_transCounter == 0)
            {
                _trans?.Commit();
                _trans?.Dispose();
                _trans = null;
            }
        }

        /// <summary>
        /// 回滚数据库的事务
        /// </summary>
        public void Rollback()
        {
            try
            {
                if (--_transCounter == 0)
                {
                    _trans?.Rollback();
                    _trans?.Dispose();
                    _trans = null;
                }
            }
            finally
            {
                if (_transCounter == 0)
                    _trans = null;
            }
        }
        #endregion

        #region Execute
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public int Execute(string sql, IDictionary<string, object> paramList = null)
            => DbConnection.Execute(sql, paramList);

        /// <summary>
        /// 执行SQL语句，并返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, IDictionary<string, object> paramList = null)
            => DbConnection.ExecuteScalar(sql, paramList);
        #endregion

        #region Query
        /// <summary>
        /// 执行SQL语句，并返回查询结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, IDictionary<string, object> paramList = null)
            => DbConnection.Query<T>(sql, paramList);
        #endregion

        #region DataTable
        /// <summary>
        /// 执行SQL语句，并返回查询结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public DataTable Query(string sql, IDictionary<string, object> paramList = null)
        {
            DataTable dt = new DataTable();
            var reader = DbConnection.ExecuteReader(sql, paramList);
            dt.Load(reader);
            return dt;
        }
        #endregion

        /// <summary>
        /// 创建临时表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string CreateTemporaryTable(string tableName)
            => driver.CreateTemporaryTable(DbConnection, tableName);

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName"></param>
        public void DropTable(string tableName)
        {
            var sql = $"DROP TABLE IF EXISTS {tableName}";
            DbConnection.Execute(sql);
        }

        /// <summary>
        /// 释放连接
        /// </summary>
        public void Dispose()
            => DbConnection.Dispose();

        /// <summary>
        /// 拷贝数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        public void BulkCopy(DataTable dataTable, string tableName)
            => driver.BulkCopy(DbConnection, dataTable, tableName);
    }
}
