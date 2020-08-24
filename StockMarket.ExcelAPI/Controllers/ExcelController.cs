using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using System.Text;
using StockMarket.ExcelAPI.DBAccess;
using StockMarket.ExcelAPI.Models;

namespace StockMarket.ExcelAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Excel")]
    public class StockController : Controller
    {
        //private readonly IHostingEnvironment _hostingEnvironment;
        StockDBContext _db = new StockDBContext();

        //public StockController(IHostingEnvironment hostingEnvironment, StockMarketDBContext db)
        //{
        //    //_hostingEnvironment = hostingEnvironment;
        //    //_db = db;
        //}


        [HttpGet]
        [Route("ImportStock/{*filePath}")]
        public void ImportStock(string filename)
        {
            string filePath = @"C:\Users\Teertho\Downloads\Batch2_Phase3-master\Batch2_Phase3-master\StockMarket\Upload\" + filename;

            //  string rootFolder = _hostingEnvironment.WebRootPath;
            // string fileName = @"ImportCustomers.xlsx";
            //  FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            FileInfo file = new FileInfo(filePath);
            string fileName = file.Name;
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                int totalRows = workSheet.Dimension.Rows;

                List<StockPrice> stockPrices = new List<StockPrice>();

                for (int i = 2; i <= totalRows; i++)
                {
                    stockPrices.Add(new StockPrice
                    {
                        CompanyCode = int.Parse(workSheet.Cells[i, 1].Value.ToString().Trim()),
                        StockExchange = workSheet.Cells[i, 2].Value.ToString().Trim(),
                        CurrentPrice = double.Parse(workSheet.Cells[i, 3].Value.ToString().Trim()),
                        Date = DateTime.Parse(workSheet.Cells[i, 4].Value.ToString().Trim()),
                        Time = workSheet.Cells[i, 5].Value.ToString(),
                    });
                }

                _db.StockPrices.AddRange(stockPrices);
                _db.SaveChanges();
            }
        }
    }
}