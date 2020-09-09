using StockMarket.UserAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.UserAPI.DBAccess;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.VisualBasic.CompilerServices;

namespace StockMarket.UserAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private StockDBContext context;
        public UserRepository(StockDBContext context)
        {
            this.context = context;
        }
        public List<Company> GetAllCompanies()
        {
            return context.Companies
                .Where(c => c.IsActive == true)
                .ToList();
        }
        public Company GetCompanyById(int id)
        {
            return context.Companies
                .Where(c => c.CompanyCode == id && c.IsActive == true)
                .FirstOrDefault();
        }

        public List<StockPrice> GetStockPrices(int companyCode, DateTime startDate, DateTime endDate)
        {
            return context.StockPrices
                .Where(sp =>
                    sp.CompanyCode == companyCode &&
                    sp.Date >= startDate &&
                    sp.Date <= endDate
                ).OrderBy(sp => sp.Date).ToList();
        }

        public List<Company> SearchCompanies(string query)
        {
            List<Company> companies = context.Companies.Where(c =>
                c.IsActive == true && (
                c.CompanyCode.ToString().ToLower().StartsWith(query.ToLower()) ||
                c.CompanyName.ToLower().StartsWith(query.ToLower())
            )).ToList();

            if (companies.Any()) return companies;

            return context.Companies.Where(c =>
                c.IsActive == true && (
                c.CompanyCode.ToString().ToLower().Contains(query.ToLower()) ||
                c.CompanyName.ToLower().Contains(query.ToLower())
            )).ToList();
        }
        public bool IsActive(int companyCode)
        {
            return context.Companies
                .Where(c =>
                    c.CompanyCode == companyCode &&
                    c.IsActive == true
                ).Any();
        }

    }
}
