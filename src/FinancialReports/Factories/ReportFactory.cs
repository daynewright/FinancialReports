using System;
using System.Collections.Generic;
using FinancialReports.Data;
using FinancialReports.Models;
using Microsoft.Data.Sqlite;

namespace FinancialReports.Factories
{
    public class ReportFactory
    {
        public Revenue SingleProduct { get; set; }
        public static List<Revenue> ListProducts { get; set; } = new List<Revenue>();

        private static BangazonConnection _connection = new BangazonConnection();
        public static List<Revenue> getWeeklyReport()
        {
            BangazonConnection connection = _connection;

            connection.execute(@"SELECT 
                ProductName, ProductRevenue, PurchaseDate
                FROM Revenue WHERE PurchaseDate >= DATE('now', 'weekday 0', '-7 days')",
            (SqliteDataReader reader) => {
                while (reader.Read())
                {
                    ListProducts.Add( new Revenue {
                        ProductName = reader[0].ToString(),
                        ProductRevenue = reader.GetInt32(1),
                        PurchaseDate = Convert.ToDateTime(reader[2].ToString())
                    });
                }
            });
            return ListProducts;
        }
        public static List<Revenue> getMonthlyReport()
        {
            BangazonConnection connection = _connection;
            connection.execute(@"SELECT
                ProductName, ProductRevenue, PurchaseDate
                FROM Revenue WHERE PurchaseDate BETWEEN DATE('now','start of month') AND DATE('now')",
            (SqliteDataReader reader) => {
                while (reader.Read())
                {
                    ListProducts.Add( new Revenue {
                        ProductName = reader[0].ToString(),
                        ProductRevenue = reader.GetInt32(1),
                        PurchaseDate = Convert.ToDateTime(reader[2].ToString())
                    });
                }
            });
            return ListProducts;
        }
        public static List<Revenue> getCustomerReport()
        {
            throw new NotImplementedException();
        }
        public static List<Revenue> getQuartlyReport()
        {
            throw new NotImplementedException();
        }
    }
}
