using FinancialReports.Data;
using FinancialReports.Models;
using FinancialReports.Factories;
using Xunit;
using System.Collections.Generic;
using System;

namespace FinancialReportsTests
{
    public class ReportFactoryTests
    {
        [Fact]
        public void CanGetWeeklyPurchasedProducts()
        {
            BangazonConnection connection = new BangazonConnection();
            List<Revenue> weeklyReport = new List<Revenue>();
            
            weeklyReport = ReportFactory.getWeeklyReport();

            foreach(Revenue product in weeklyReport)
            {
                Assert.NotNull(product);
                Assert.True(product.PurchaseDate >= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek));
            }
        }
    }
}
