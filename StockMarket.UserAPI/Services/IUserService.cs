using StockMarket.UserAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.UserAPI.Services
{
   public interface IUserService
    {
        public Company GetCompanyByCompanyCode(int id);
        public List<Company> GetCompanies();
        public List<Company> GetCompanies(string query);
        public List<StockPrice> GetCompanyDetails(int companyCode, DateTime start, DateTime end);
        public bool IsActive(int companyCode);
    }
}
