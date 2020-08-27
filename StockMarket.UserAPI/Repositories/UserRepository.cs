﻿using StockMarket.UserAPI.Models;
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
        public Company SearchCompany(string name)
        {
            return context.Companies.SingleOrDefault(i => i.CompanyName == name|| i.CompanyCode == int.Parse(name));
        }
        public IEnumerable<StockPrice> SearchStocksofCompany(Company company)
        {
            return company.StockPrices.ToList();
        }
        public IEnumerable<StockPrice> ComparePricesOfCompanies(DateTime dateTime)
        {
            return context.StockPrices.Where(x => x.Date.Equals(dateTime)).ToList();
        }
    }
}
