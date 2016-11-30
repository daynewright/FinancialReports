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
        [Fact]
        public void CanGetMonthlyPurchasedProducts()
        {
            BangazonConnection connection = new BangazonConnection();
            List<Revenue> monthlyReport = new List<Revenue>();

            monthlyReport = ReportFactory.getMonthlyReport();

            foreach(Revenue product in monthlyReport)
            {
                Assert.True(product.PurchaseDate.Month == DateTime.Today.Month);
            }
        }
        [Fact]
        public void CanGetQuarterlyPurchasedProducts()
        {
            BangazonConnection connection = new BangazonConnection();
            List<Revenue> quarterlyReport = new List<Revenue>();
            int currentMonth = DateTime.Now.Month;
            int currentQuarter = 4;

            quarterlyReport = ReportFactory.getQuartlyReport();

            if(currentMonth < 4)
            {
                currentQuarter = 1;
            } 
            else if(currentMonth > 3 && currentMonth < 7)
            {
                currentQuarter = 2;
            } 
            else if(currentMonth > 6 && currentMonth < 10)
            {
                currentQuarter = 3;
            }

            foreach(Revenue product in quarterlyReport)
            {
                Assert.True(product.Quarter == currentQuarter);
            }
        }
    }
}
