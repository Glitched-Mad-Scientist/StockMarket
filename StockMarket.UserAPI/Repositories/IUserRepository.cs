using StockMarket.UserAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.UserAPI.Repositories
{
    public interface IUserRepository
    {
        public Company SearchCompany(string name);
        public IEnumerable<StockPrice> SearchStocksofCompany(Company company);
    }
}
