using System;
using System.Collections.Generic;
using StockMarket.ExcelAPI.Models;

namespace StockMarket.ExcelAPI.Repositories {
    public interface IExcelRepositiory {
        public void AddStockPrices(List<StockPrice> stockPrices);
        public bool isCompany(int id);
        List<StockPrice> GetStockPrices(int companyCode, DateTime startDate, DateTime endDate);
    }
}
