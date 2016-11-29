using System;
using System.Collections.Generic;
using FinancialReports.Factories;
using System.Linq;
using System.Threading.Tasks;
using FinancialReports.Models;
using System.Globalization;

namespace FinancialReports.Actions
{
    public class ReportAction
    {
        private static List<Revenue> prods = new List<Revenue>();
        public static void printWeeklyReport()
        {
            Console.WriteLine(@"
==========================
BANGAZON FINANCIAL REPORTS
==========================

WEEKLY REPORT

Product                                          Amount
-------------------------------------------------------");

            prods = ReportFactory.getWeeklyReport();

            foreach(Revenue prod in prods)
            {
                Console.WriteLine("{0,-25} {1,30}", prod.ProductName, prod.ProductRevenue.ToString("C", new CultureInfo("en-US")));

            }
            Console.WriteLine("");
            Console.WriteLine("Press any key to return to the Main Menu.");
            Console.Read();
        }
        public static void printMonthlyReport()
        {
            Console.WriteLine(@"
==========================
BANGAZON FINANCIAL REPORTS
==========================

MONTHLY REPORT

Product                                          Amount
-------------------------------------------------------");

            prods = ReportFactory.getMonthlyReport();

            foreach(Revenue prod in prods)
            {
                Console.WriteLine("{0,-25} {1,30}", prod.ProductName, prod.ProductRevenue.ToString("C", new CultureInfo("en-US")));
            }
            Console.WriteLine("");
            Console.WriteLine("Press any key to return to the Main Menu.");
            Console.Read();
        }
    }
}
