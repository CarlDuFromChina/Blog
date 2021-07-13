using Blog.Core.Data.Driver;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Data.DbClient
{
    public class DbConnectionFactory
    {
        public static DbConnection GetDbConnection(DriverType driverType, string connectionString)
        {
            switch (driverType)
            {
                case DriverType.Postgresql:
                    return new NpgsqlConnection(connectionString);
                case DriverType.Mysql:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
