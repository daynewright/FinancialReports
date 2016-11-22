using FinancialReports.Data;
using FinancialReports.Models;
using FinancialReports.Factories;
using System.Collections.Generic;
using Xunit;

namespace FinancialReportsTests
{
    public class ReportFactoryTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CanGetSingleProductFromRevenueTable(int id)
        {
            BangazonConnection connection = new BangazonConnection();
            ReportFactory getProduct = new ReportFactory();

            Revenue SingleProduct = getProduct.get(id);

            Assert.NotNull(SingleProduct);

            Assert.NotNull(SingleProduct.Id);
            Assert.NotNull(SingleProduct.CustomerFirstName);
            Assert.NotNull(SingleProduct.CustomerLastName);
            Assert.NotNull(SingleProduct.CustomerAddress);
            Assert.NotNull(SingleProduct.CustomerZipCode);
            Assert.NotNull(SingleProduct.ProductCost);
            Assert.NotNull(SingleProduct.ProductRevenue);
            Assert.NotNull(SingleProduct.ProductSupplierState);
            Assert.NotNull(SingleProduct.PurchaseDate);
        }
    }
}
