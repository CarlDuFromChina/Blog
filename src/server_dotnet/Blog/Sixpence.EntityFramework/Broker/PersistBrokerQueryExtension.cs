using Sixpence.Common.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Broker
{
    public static class PersistBrokerQueryExtension
    {
        /// <summary>
        /// 通过lambda表达式的方式执行数据库事务
        /// </summary>
        public static void ExecuteTransaction(this IPersistBroker broker, Action func)
        {
            try
            {
                broker.DbClient.Open();
                broker.DbClient.BeginTransaction();

                func?.Invoke();

                broker.DbClient.CommitTransaction();
            }
            catch (Exception ex)
            {
                broker.DbClient.Rollback();
                throw ex;
            }
            finally
            {
                broker.DbClient.Close();
            }
        }

        /// <summary>
        /// 通过lambda表达式的方式执行数据库事务
        /// </summary>
        public static T ExecuteTransaction<T>(this IPersistBroker broker, Func<T> func, string transId = null)
        {
            try
            {
                broker.DbClient.Open();
                broker.DbClient.BeginTransaction();

                var t = default(T);

                if (func != null)
                {
                    t = func();
                }

                broker.DbClient.CommitTransaction();

                return t;
            }
            catch (Exception ex)
            {
                broker.DbClient.Rollback();
                throw ex;
            }
            finally
            {
                broker.DbClient.Close();
            }

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broker"></param>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(this IPersistBroker broker, string sql, IDictionary<string, object> paramList = null)
        {
            return broker.DbClient.Query<T>(sql, paramList);
        }

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broker"></param>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static int QueryCount(this IPersistBroker broker, string sql, IDictionary<string, object> paramList = null)
        {
            return ConvertUtil.ConToInt(broker.ExecuteScalar(sql, paramList));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static DataTable Query(this IPersistBroker broker, string sql, IDictionary<string, object> paramList = null)
        {
            return broker.DbClient.Query(sql, paramList);
        }
    }
}
