using System.Collections.Generic;
using FinancialReports.Data;
using FinancialReports.Models;
using Microsoft.Data.Sqlite;

namespace FinancialReports.Factories
{
    public class ReportFactory
    {
        public Revenue SingleProduct { get; set; }
        public List<Revenue> ListProducts { get; set; }


        public Revenue get(int id)
        {
            BangazonConnection connection = new BangazonConnection();

            connection.execute(@"SELECT * FROM Revenue WHERE Id = " + id,
            (SqliteDataReader reader) =>
            {
                while (reader.Read())
                {
                    SingleProduct = new Revenue
                    {
                        Id = reader.GetInt32(0),
                        ProductName = reader[1].ToString(),
                        ProductCost = reader.GetInt32(2),
                        ProductRevenue = reader.GetInt32(3),
                        ProductSupplierState = reader[4].ToString(),
                        CustomerFirstName = reader[5].ToString(),
                        CustomerLastName = reader[6].ToString(),
                        CustomerAddress = reader[7].ToString(),
                        CustomerZipCode = reader.GetInt32(8),
                        PurchaseDate = reader.GetDateTime(9)
                    };
                }
            });
            return SingleProduct;
        }

        public List<Revenue> getByDates(string startDate, string endDate)
        {
            BangazonConnection connection = new BangazonConnection();
            connection.execute($@"SELECT * FROM Revenue WHERE PurchaseDate < {endDate} & PurchaseDate > {startDate}",
                (SqliteDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        SingleProduct = new Revenue
                        {
                            Id = reader.GetInt32(0),
                            ProductName = reader[1].ToString(),
                            ProductCost = reader.GetInt32(2),
                            ProductRevenue = reader.GetInt32(3),
                            ProductSupplierState = reader[4].ToString(),
                            CustomerFirstName = reader[5].ToString(),
                            CustomerLastName = reader[6].ToString(),
                            CustomerAddress = reader[7].ToString(),
                            CustomerZipCode = reader.GetInt32(8),
                            PurchaseDate = reader.GetDateTime(9)
                        };

                        ListProducts.Add(SingleProduct);
                    }
                });
            return ListProducts; 
        }
    }
}
