using StockMarket.AdminAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.AdminAPI.Services
{
   public interface IAdminService
    {
        void AddCompany(Company company);
        public Company CreateCompany(string sector, string cname, long turnover, string ceo, string bod, string se, string sc, string desc);
        Company ValidateName(string cname);
        Company ValidateCid(int cid);
        public void DeleteCompany(Company item);
        public void UpdateCompany(int CId, string sector, string cname, long turnover, string ceo, string bod, string se, string sc, string desc);
        public IPO AddIPO(IPO iPO);
        public IPO UpdateIPO(IPO iPO);
        public List<DateTime> GetMissingStockPriceDates(int companyCode,DateTime startDate,DateTime endDate);
        public Company ActivateCompany(int companyCode);
        public Company DeactivateCompany(int companyCode);
        public List<Company> GetCompanies();
    }
}
