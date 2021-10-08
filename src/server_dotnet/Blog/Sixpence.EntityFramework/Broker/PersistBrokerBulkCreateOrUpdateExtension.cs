using Sixpence.Core;
using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Broker
{
    /// <summary>
    /// PersistBroker 批量创建或更新扩展
    /// </summary>
    public static class PersistBrokerBulkCreateOrUpdateExtension
    {
        /// <summary>
        /// 批量创建
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="broker"></param>
        /// <param name="dataList"></param>
        public static void BulkCreate<TEntity>(this IPersistBroker broker, List<TEntity> dataList) where TEntity : BaseEntity, new()
        {
            var client = broker.DbClient;

            if (dataList.IsEmpty()) return;

            var tableName = dataList[0].EntityName;
            var dt = client.Query($"select * from {tableName}");
            BulkCreate(broker, dataList.ToDataTable(dt.Columns), tableName);
        }

        /// <summary>
        /// 批量创建数据
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        public static void BulkCreate(this IPersistBroker broker, DataTable dataTable, string tableName)
        {
            if (dataTable.IsEmpty()) return;

            var client = broker.DbClient;

            // 1. 创建临时表
            var tempName = client.CreateTemporaryTable(tableName);

            // 2. 拷贝数据到临时表
            client.BulkCopy(dataTable, tempName);

            // 3. 将临时表数据插入到目标表中
            client.Execute(string.Format("INSERT INTO {0} SELECT * FROM {1} WHERE NOT EXISTS(SELECT 1 FROM {0} WHERE {0}.{2}id = {1}.{2}id)", tableName, tempName, tableName));

            // 4. 删除临时表
            client.DropTable(tempName);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="broker"></param>
        /// <param name="dataList"></param>
        public static void BulkUpdate<TEntity>(this IPersistBroker broker, List<TEntity> dataList) where TEntity : BaseEntity, new()
        {
            if (dataList.IsEmpty()) return;

            var client = broker.DbClient;
            var mainKeyName = new TEntity().MainKeyName; // 主键
            var tableName = new TEntity().EntityName; // 表名

            // 1. 创建临时表
            var tempTableName = client.CreateTemporaryTable(tableName);

            // 2. 查询临时表结构
            var dt = client.Query($"SELECT * FROM {tempTableName}");

            // 3. 拷贝数据到临时表
            client.BulkCopy(dataList.ToDataTable(dt.Columns), tempTableName);

            // 4. 获取更新字段
            var updateFieldList = new List<string>();
            foreach (DataColumn column in dt.Columns)
            {
                // 主键去除
                if (!column.ColumnName.Equals(mainKeyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    updateFieldList.Add(column.ColumnName);
                }
            }

            // 5. 拼接Set语句
            var updateFieldSql = updateFieldList.Select(item => string.Format(" {1} = {0}.{1} ", tempTableName, item)).Aggregate((a, b) => a + " , " + b);

            // 6. 更新
            client.Execute($@"
UPDATE {tableName}
SET {updateFieldSql} FROM {tempTableName}
WHERE {tableName}.{mainKeyName} = {tempTableName}.{mainKeyName}
AND {tempTableName}.{mainKeyName} IS NOT NULL
");

            // 7. 删除临时表
            client.DropTable(tempTableName);
        }

        /// <summary>
        /// 批量创建或更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="broker"></param>
        /// <param name="dataList"></param>
        public static void BulkCreateOrUpdate<TEntity>(this IPersistBroker broker, List<TEntity> dataList, List<string> updateFieldList = null) where TEntity : BaseEntity, new()
        {
            if (dataList.IsEmpty()) return;

            var client = broker.DbClient;
            var mainKeyName = new TEntity().MainKeyName; // 主键
            var tableName = new TEntity().EntityName; // 表名

            // 1. 创建临时表
            var tempTableName = client.CreateTemporaryTable(tableName);

            // 2. 查询临时表结构
            var dt = client.Query($"SELECT * FROM {tempTableName}");

            // 3. 拷贝数据到临时表
            client.BulkCopy(dataList.ToDataTable(dt.Columns), tempTableName);

            // 4. 获取更新字段
            if (updateFieldList.IsEmpty())
            {
                updateFieldList = new List<string>();
                foreach (DataColumn column in dt.Columns)
                {
                    // 主键去除
                    if (!column.ColumnName.Equals(mainKeyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        updateFieldList.Add(column.ColumnName);
                    }
                }
            }

            // 5. 拼接Set语句
            var updateFieldSql = updateFieldList.Select(item => string.Format(" {1} = {0}.{1} ", tempTableName, item)).Aggregate((a, b) => a + " , " + b);

            // 6. 更新
            client.Execute($@"
UPDATE {tableName}
SET {updateFieldSql} FROM {tempTableName}
WHERE {tableName}.{mainKeyName} = {tempTableName}.{mainKeyName}
AND {tempTableName}.{mainKeyName} IS NOT NULL
");
            // 7. 新增
            client.Execute($@"
INSERT INTO {tableName}
SELECT * FROM {tempTableName}
WHERE NOT EXISTS(SELECT 1 FROM {tableName} WHERE {tableName}.{mainKeyName} = {tempTableName}.{mainKeyName})
AND {tempTableName}.{mainKeyName} IS NOT NULL
");

            // 8. 删除临时表
            client.DropTable(tempTableName);
        }
    }
}
