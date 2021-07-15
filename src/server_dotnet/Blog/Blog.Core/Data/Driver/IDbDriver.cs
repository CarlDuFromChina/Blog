using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Data.Driver
{
    [ServiceRegister]
    public interface IDbDriver
    {
        DbConnection GetDbConnection(string connectionString);
        string CreateTable(string name);
        string CreateRole(string name);
        string DropRole(string name);
        string CreateUser(string name);
        string DropUser(string name);
        string QueryRole(string name);
        string GetDataBase(string name);
        string GetTable(string tableName);
        string GetAddColumnSql(string tableName, List<Column> columns);
        string GetDropColumnSql(string tableName, List<Column> columns);
        string CreateTemporaryTable(IDbConnection conn, string tableName);
        void BulkCopy(IDbConnection conn, DataTable dataTable, string tableName);
    }
}
