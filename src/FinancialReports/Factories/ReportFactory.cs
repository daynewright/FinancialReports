using System;
using System.Collections.Generic;
using FinancialReports.Data;
using FinancialReports.Models;
using Microsoft.Data.Sqlite;

namespace FinancialReports.Factories
{
    public class ReportFactory
    {
        public static List<Revenue> ListCustomers { get; set; } = new List<Revenue>();
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
        public static List<Revenue> getQuartlyReport()
        {
            BangazonConnection connection = _connection;
            connection.execute(@"SELECT
                                ProductName,
                                ProductRevenue,
                                PurchaseDate,
                                CASE
                                WHEN cast(strftime('%m', PurchaseDate) as integer) BETWEEN 1 AND 3 THEN 1
                                WHEN cast(strftime('%m', PurchaseDate) as integer) BETWEEN 4 and 6 THEN 2
                                WHEN cast(strftime('%m', PurchaseDate) as integer) BETWEEN 7 and 9 THEN 3
                                ELSE 4 END as Quarter,
                                CASE
                                WHEN cast(strftime('%m', DATE('now')) as integer) BETWEEN 1 and 3 THEN 1
                                WHEN cast(strftime('%m', DATE('now')) as integer) BETWEEN 4 and 6 THEN 2
                                WHEN cast(strftime('%m', DATE('now')) as integer) BETWEEN 7 and 9 THEN 3
                                ELSE 4 END as CurrentQuarter
                                FROM Revenue
                                WHERE Quarter == CurrentQuarter",
            (SqliteDataReader reader) => {
                while (reader.Read())
                {
                    ListProducts.Add( new Revenue {
                        ProductName = reader[0].ToString(),
                        ProductRevenue = reader.GetInt32(1),
                        PurchaseDate = Convert.ToDateTime(reader[2].ToString()),
                        Quarter = reader.GetInt32(3)
                    });
                }
            });
            return ListProducts;
        }
        public static List<Revenue> getCustomerReport()
        {
            BangazonConnection connection = _connection;
            connection.execute(@"SELECT
                                CustomerFirstName,
                                CustomerLastName,
                                SUM(ProductRevenue) AS 'GrossSales'
                                FROM Revenue
                                GROUP BY CustomerFirstName || CustomerLastName
                                ORDER BY GrossSales DESC",
            (SqliteDataReader reader) => {
                while (reader.Read())
                {
                    ListCustomers.Add( new Revenue {
                        CustomerFirstName = reader[0].ToString(),
                        CustomerLastName = reader[1].ToString(),
                        ProductRevenue = reader.GetInt32(2)
                    });
                }
            });
            return ListCustomers;
        }
    }
}
