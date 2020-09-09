using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.UserAPI.Repositories;
using StockMarket.UserAPI.Models;
namespace StockMarket.UserAPI.Services
{
    public class UserService:IUserService
    {
        private IUserRepository userRepository;
        public UserService(IUserRepository repo)
        {
            userRepository = repo;    
        }

        public Company GetCompanyByCompanyCode(int compantCode)
        {
            return userRepository.GetCompanyById(compantCode);
        }

        public List<Company> GetCompanies()
        {
            return userRepository.GetAllCompanies();
        }

        public List<Company> GetCompanies(string query)
        {
            return userRepository.SearchCompanies(query);
        }

        public List<StockPrice> GetCompanyDetails(
            int companyCode,
            DateTime startDate,
            DateTime endDate
        )
        {
            List<StockPrice> stockPrices = userRepository.GetStockPrices(
                companyCode,
                startDate,
                endDate
            );

            return stockPrices;
        }

        public bool IsActive(int companyCode)
        {
            return userRepository.IsActive(companyCode);
        }
    }
}
