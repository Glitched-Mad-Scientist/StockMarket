using System;
using System.Collections.Generic;
using System.IO;
using StockMarket.ExcelAPI.Models;
using StockMarket.ExcelAPI.Repositories;
using OfficeOpenXml;

namespace StockMarket.ExcelAPI.Services {
    public class ExcelService:IExcelService {
        IExcelRepositiory repo;
        public ExcelService(IExcelRepositiory repo) {
            this.repo = repo;
        }

        public List<StockPrice> ImportSpreadsheet(string filePath, string worksheetName) {
            FileInfo file = new FileInfo(filePath);
            List<StockPrice> stockPrices = new List<StockPrice>();

            using (ExcelPackage package = new ExcelPackage(file)) {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[worksheetName];
                if (
                    worksheet.Dimension.Columns != 5 ||
                    worksheet.Cells[1,1].Value.ToString().Trim().ToLower() != "company code" ||
                    worksheet.Cells[1,2].Value.ToString().Trim().ToLower() != "stock exchange" ||
                    worksheet.Cells[1,3].Value.ToString().Trim().ToLower() != "current price" ||
                    worksheet.Cells[1,4].Value.ToString().Trim().ToLower() != "date" ||
                    worksheet.Cells[1,5].Value.ToString().Trim().ToLower() != "time"
                    ) throw new FormatException(
                        "Worksheet need to be in required format: 'Company Code | Stock Exchange | Current Price | Date | Time'"
                        );

                int totalRows = worksheet.Dimension.Rows;
                if (totalRows < 2) throw new FormatException("There must be atleast one data row");

                for (int i = 2; i <= totalRows; i++) {
                    int companyCode = Int32.Parse(worksheet.Cells[i, 1].Value.ToString().Trim());

                    if (!repo.isCompany(companyCode)) 
                        throw new ArgumentException("No company identified by "+ companyCode+". Aborting!");

                    stockPrices.Add(new StockPrice {
                        CompanyCode = companyCode,
                        StockExchange = worksheet.Cells[i, 2].Value.ToString().Trim(),
                        CurrentPrice = double.Parse(worksheet.Cells[i, 3].Value.ToString().Trim()),
                        Date = DateTime.Parse(worksheet.Cells[i, 4].Value.ToString().Trim()),
                        Time = worksheet.Cells[i, 5].Value.ToString(),
                    });
                }
            }

            repo.AddStockPrices(stockPrices);

            return stockPrices;
        }

        public FileStream ExportData(List<int> companyCodes, DateTime startDate, DateTime endDate) {
            string filepath = Path.Combine("ExcelFiles\\Downloads", "Export.xlsx");
            using (ExcelPackage package = new ExcelPackage()) {

                foreach(int companyCode in companyCodes) {
                    List<StockPrice> stockPrices = repo.GetStockPrices(
                        companyCode,
                        startDate,
                        endDate
                    );
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(companyCode.ToString());
                    int totalRows = stockPrices.Count;
                    worksheet.Cells[1, 1].Value = "Company Code";
                    worksheet.Cells[1, 2].Value = "Stock Exchange";
                    worksheet.Cells[1, 3].Value = "Current Price";
                    worksheet.Cells[1, 4].Value = "Date";
                    worksheet.Cells[1, 5].Value = "Time";
                    int i = 0;
                    for (int row = 2; row <= totalRows + 1; row++) {
                        worksheet.Cells[row, 1].Value = stockPrices[i].CompanyCode;
                        worksheet.Cells[row, 2].Value = stockPrices[i].StockExchange;
                        worksheet.Cells[row, 3].Value = stockPrices[i].CurrentPrice;
                        worksheet.Cells[row, 4].Value = stockPrices[i].Date;
                        worksheet.Cells[row, 5].Value = stockPrices[i].Time;
                        i++;
                    }
                }
                package.SaveAs(new FileInfo(filepath));
            }
            var file = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            return file;
        }
    }
}
