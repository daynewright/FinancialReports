using Microsoft.Data.Sqlite;
using System;

namespace FinancialReports.Data
{
    public class BangazonConnection
    {
        private string _path = "Data Source=" + System.Environment.GetEnvironmentVariable("REPORTING_DB_PATH");
        public string path { get; }

        public BangazonConnection()
        {
            this.path = _path;
        }

        public void execute(string query, Action<SqliteDataReader> handler)
        {

            SqliteConnection dbcon = new SqliteConnection(_path);

            dbcon.Open();
            SqliteCommand dbcmd = dbcon.CreateCommand();
            dbcmd.CommandText = query;

            using (var reader = dbcmd.ExecuteReader())
            {
                handler(reader);
            }

            dbcmd.Dispose();
            dbcon.Close();
        }

    }
}
