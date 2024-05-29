using System;
using System.Data;
using System.Data.SqlClient;
using Y.Infrastructure.Library.Core.Extensions;

namespace Y.Infrastructure.Library.Core.DbHelper
{
    public class ConnectionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static IDbConnection CreateConnection(string dbType, string conn)
        {
            if (dbType.IsNullOrWhiteSpace())
                throw new ArgumentNullException("未传入数据库类型");
            if (conn.IsNullOrWhiteSpace())
                throw new ArgumentNullException("未传入数据库链接字符串");
            var dbtype = GetDatabaseType(dbType);
            return CreateConnection(dbtype, conn);
        }


        public static IDbConnection CreateConnection(DatabaseType dbType, string conn)
        {
            IDbConnection connection = null;
            if (conn.IsNullOrWhiteSpace())
                throw new ArgumentNullException("未传入数据库类型");

            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection = new SqlConnection(conn);
                    break;
                //case DatabaseType.MySQL:
                //    connection = new MySqlConnection(conn);
                //    Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.MySQL);
                //    break;
                //case DatabaseType.SQLite:
                //    connection = new SQLiteConnection(conn);
                //    Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.SQLite);
                //    break;
                //case DatabaseType.PostgreSQL:
                //    connection = new NpgsqlConnection(conn);
                //    break;
                default:
                    throw new ArgumentNullException($"目前不支持的{dbType.ToString()}数据库类型");
            }

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;
        }


        public static DatabaseType GetDatabaseType(string dbType)
        {
            if (dbType.IsNullOrWhiteSpace())
                throw new ArgumentNullException("未传入数据库类型");
            DatabaseType returnValue = DatabaseType.SqlServer;
            foreach (DatabaseType dbtype in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbtype.ToString().Equals(dbType, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbtype;
                    break;
                }
            }

            return returnValue;
        }
    }
}