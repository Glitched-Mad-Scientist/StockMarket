using StockMarket.UserAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.UserAPI.Repositories
{
    public interface IUserRepository
    {
        public List<Company> GetAllCompanies();
        public Company GetCompanyById(int id);
        public List<Company> SearchCompanies(string query);
        public List<StockPrice> GetStockPrices(int companyCode, DateTime start, DateTime end);
        public bool IsActive(int companyCode);
    }
}
