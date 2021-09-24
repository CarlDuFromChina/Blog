using Dapper;
using Newtonsoft.Json.Linq;
using Npgsql;
using Sixpence.Core;
using Sixpence.Core.Utils;
using Sixpence.EntityFramework.DbClient;
using Sixpence.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Driver
{
    public class PostgresqlDriver : IDbDriver
    {
        public DbConnection GetDbConnection(string connectionString)
        {
            return DbConnectionFactory.GetDbConnection(DriverType.Postgresql, connectionString);
        }

        public string CreateRole(string name)
        {
            return $"CREATE ROLE {name}";
        }

        public string CreateTable(string name)
        {
            var sql = $@"
CREATE TABLE {name}
(
{name}id VARCHAR(100) PRIMARY KEY
)
";
            return sql;
        }

        public string CreateTemporaryTable(IDbConnection conn, string tableName)
        {
            var newTableName = tableName + Guid.NewGuid().ToString().Replace("-", "");
            conn.Execute($@"CREATE TEMP TABLE {newTableName} ON COMMIT DROP AS SELECT * FROM {tableName} WHERE 1!=1;");
            return newTableName;
        }

        public string CreateUser(string name)
        {
            return $"CREATE USER {name}";
        }

        public string DropRole(string name)
        {
            return $"DROP ROLE {name}";
        }

        public string DropUser(string name)
        {
            return $"DROP User {name}";
        }

        public string GetAddColumnSql(string tableName, List<Column> columns)
        {
            var sql = new StringBuilder();
            var tempSql = $@"ALTER TABLE {tableName}";
            foreach (var item in columns)
            {
                var itemSql = $"{tempSql} ADD COLUMN IF NOT EXISTS {item.Name} {item.Type.GetDescription()}{(item.Length != null ? $"({item.Length})" : "")} {(item.IsRequire == true ? " NOT NULL" : "")};\r\n";
                sql.Append(itemSql);
            }
            return sql.ToString();
        }

        public string GetDataBase(string name)
        {
            return $@"
SELECT u.datname
FROM pg_catalog.pg_database u where u.datname='{name}';
";
        }

        public string GetDropColumnSql(string tableName, List<Column> columns)
        {
            var sql = $@"
ALTER TABLE {tableName}
";
            var count = 0;
            foreach (var item in columns)
            {
                var itemSql = $"DROP COLUMN {item.Name} {(++count == columns.Count ? ";" : ",")}";
                sql += itemSql;
            }
            return sql;
        }

        public string GetTable(string tableName)
        {
            return $@"
SELECT * FROM pg_tables
WHERE schemaname = 'public' AND tablename = '{tableName}'";
        }

        public string QueryRole(string name)
        {
            return $@"
SELECT * FROM pg_roles
WHERE rolname = '{name}'";
        }

        public void BulkCopy(IDbConnection conn, DataTable dataTable, string tableName)
        {
            var columnNameList = (from DataColumn column in dataTable.Columns select column.ColumnName.ToLower())
                .ToList();
            var columnNames = columnNameList.Aggregate((l, n) => l + "," + n);
            var cn = conn as NpgsqlConnection;
            if (conn == null) return;

            using (var writer = cn.BeginBinaryImport($"COPY {tableName}({columnNames}) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    writer.StartRow();
                    foreach (var columnName in columnNameList)
                    {
                        if (string.IsNullOrWhiteSpace(dr[columnName].ToString()))
                        {
                            writer.WriteNull();
                        }
                        else
                        {
                            if (dataTable.Columns[columnName].DataType == typeof(int) || dataTable.Columns[columnName].DataType == typeof(long))
                                writer.Write(ConvertUtil.ConToInt(dr[columnName]), NpgsqlTypes.NpgsqlDbType.Integer);
                            else if (dataTable.Columns[columnName].DataType == typeof(decimal))
                                writer.Write(ConvertUtil.ConToDecimal(dr[columnName]), NpgsqlTypes.NpgsqlDbType.Numeric);
                            else if (dataTable.Columns[columnName].DataType == typeof(DateTime))
                                writer.Write(ConvertUtil.ConToDateTime(dr[columnName]), NpgsqlTypes.NpgsqlDbType.Timestamp);
                            else if (dataTable.Columns[columnName].DataType == typeof(JToken))
                                writer.Write(dr[columnName].ToString(), NpgsqlTypes.NpgsqlDbType.Jsonb);
                            else
                                writer.Write(dr[columnName].ToString());
                        }
                    }
                }
                writer.Complete();
            }
        }

        public void AddLimit(ref string sql, int? index, int size)
        {
            if (index.HasValue)
            {
                sql += $" LIMIT {size} OFFSET {(index - 1) * size}";
            }
            else
            {
                sql += $" LIMIT {size}";
            }
        }
    }
}
