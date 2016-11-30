using FinancialReports.Data;
using Microsoft.Data.Sqlite;
using FinancialReports.Models;
using System;
using FinancialReports.Factories;
using System.Collections.Generic;
using FinancialReports.Actions;

namespace BangazonProductRevenueReports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BangazonConnection db = new BangazonConnection();
            Revenue data = null;
            bool isActive = true;
            string userInput = "";
            
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

            while(isActive)
            {
                Console.WriteLine(@"
==========================
BANGAZON FINANCIAL REPORTS
==========================
1. Weekly Report
2. Monthly Report
3. Quarterly Report
4. Customer Revenue Report
5. Product Revenue Report
x. Exit Program");

                Console.Write("> ");

                userInput = Console.ReadLine();

                if(userInput.ToUpper() == "X")
                {
                    isActive = false;
                    break;
                }

                switch(userInput)
                {
                    case "1":
                        ReportAction.printWeeklyReport();
                        break;

                    case "2":
                        ReportAction.printMonthlyReport();
                        break;

                    case "3":
                        //dReportFactory.getQuartlyReport();
                        break;

                    case "4":
                        //ReportFactory.getCustomerReport();
                        break;

                    case "5":
                        //ReportAction.getProductReport();
                        break;
                        
                    default:
                        Console.WriteLine("You did not enter a valid menu option.  Please try again.");
                        Console.WriteLine("");
                        break;
                }
                
            }
        }
    }
}