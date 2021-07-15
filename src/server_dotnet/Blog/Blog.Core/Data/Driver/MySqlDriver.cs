using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Data.Driver
{
    public class MySqlDriver : IDbDriver
    {
        public void BulkCopy(IDbConnection conn, DataTable dataTable, string tableName)
        {
            throw new NotImplementedException();
        }

        public string CreateRole(string name)
        {
            throw new NotImplementedException();
        }

        public string CreateTable(string name)
        {
            throw new NotImplementedException();
        }

        public string CreateTemporaryTable(IDbConnection conn, string tableName)
        {
            throw new NotImplementedException();
        }

        public string CreateUser(string name)
        {
            throw new NotImplementedException();
        }

        public string DropRole(string name)
        {
            throw new NotImplementedException();
        }

        public string DropUser(string name)
        {
            throw new NotImplementedException();
        }

        public string GetAddColumnSql(string tableName, List<Column> columns)
        {
            throw new NotImplementedException();
        }

        public string GetDataBase(string name)
        {
            throw new NotImplementedException();
        }

        public DbConnection GetDbConnection(string connectionString)
        {
            throw new NotImplementedException();
        }

        public string GetDropColumnSql(string tableName, List<Column> columns)
        {
            throw new NotImplementedException();
        }

        public string GetTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public string QueryRole(string name)
        {
            throw new NotImplementedException();
        }
    }
}
