using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Y.Infrastructure.Library.Sqlite
{
    public class DbCreatedUtility
    {

        public static string CreateSqlTable(string dbName)
        {
            string sqlTb = @"CREATE TABLE [Orders](
							[Id] [int] IDENTITY(1,1) NOT NULL,
							[MerchantId] [int] NOT NULL,
							[CateId] [int] NOT NULL,
							[UserId] [int] NOT NULL,
							[GameId] [int] NOT NULL,
							[GameBetId] [int] NOT NULL,
							[GameBetItemId] [int] NOT NULL,
							[Status] [tinyint] NOT NULL,
							[BetTime] [datetime] NOT NULL,
							[RewardTime] [datetime] NOT NULL,
							[SettlementTime] [datetime] NOT NULL,
							[ModifyTime] [datetime] NOT NULL,
							[Ip] [varchar](16) NOT NULL,
							[Amount] [money] NOT NULL,
							[Bonus] [money] NOT NULL,
							[Award] [money] NOT NULL,
							[Device] [tinyint] NOT NULL,
							[Odds] [money] NOT NULL,
							[ResultId] [int] NOT NULL,
							[Stage] [tinyint] NOT NULL
						);";

            using (var connection = new System.Data.SQLite.SQLiteConnection($@"Data Source=D:\Projects\test\ConsoleApp1\ConsoleApp1\bin\Debug\netcoreapp3.1\{dbName}"))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = sqlTb;
                cmd.ExecuteNonQuery();
            }
            return string.Empty;
        }


        static public void CreateDbFile(string databaseFileName)
        {
            File.WriteAllBytes(databaseFileName, new byte[0]);
        }
    }
}
