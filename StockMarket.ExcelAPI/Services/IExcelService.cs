using System;
using System.Collections.Generic;
using System.IO;
using StockMarket.ExcelAPI.Models;

namespace StockMarket.ExcelAPI.Services {
    public interface IExcelService {
        public List<StockPrice> ImportSpreadsheet(string filePath, string worksheetName);
        FileStream ExportData(List<int> companyCodes, DateTime startDate, DateTime endDate);
    }
}
