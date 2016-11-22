using FinancialReports.Data;
using Microsoft.Data.Sqlite;
using FinancialReports.Models;
using System;

namespace BangazonProductRevenueReports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BangazonConnection db = new BangazonConnection();
            Revenue data = null;

            try
            {
                db.execute("SELECT Id FROM Revenue WHERE Id = 1000", (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        data = new Revenue
                        {
                            Id = reader.GetInt32(0)
                        };
                    }
                });
            }
            catch
            {
                DatabaseGenerator gen = new DatabaseGenerator();
                gen.CreateDatabase();
            }

        }
    }
}