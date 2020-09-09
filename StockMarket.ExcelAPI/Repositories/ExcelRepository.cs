using System;
using System.Collections.Generic;
using System.Linq;
using StockMarket.ExcelAPI.DBAccess;
using StockMarket.ExcelAPI.Models;

namespace StockMarket.ExcelAPI.Repositories {
    public class ExcelRepository:IExcelRepositiory {
        private StockDBContext context;
        public ExcelRepository(StockDBContext context) {
            this.context = context;
        }

        public void AddStockPrices(List<StockPrice> stockPrices) {
            context.StockPrices.AddRange(stockPrices);
            context.SaveChanges();
        }

        public List<StockPrice> GetStockPrices(int companyCode, DateTime startDate, DateTime endDate) {
            return context.StockPrices
                .Where(sp =>
                    sp.CompanyCode == companyCode &&
                    sp.Date >= startDate &&
                    sp.Date <= endDate
                ).OrderBy(sp => sp.Date).ToList();
        }

        public bool isCompany(int companyCode) {
            return context.Companies.Where(c => c.CompanyCode == companyCode).Any();
        }
    }
}
