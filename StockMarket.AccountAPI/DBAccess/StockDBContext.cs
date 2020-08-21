using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockMarket.AccountAPI.Models;
namespace StockMarket.AccountAPI.DBAccess
{
    public class StockDBContext:DbContext
    {
        //Entity Set
        public DbSet<User> Users { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SANTU\MSSQLSERVER2019;Initial Catalog=StockMarketDB;User ID=sa;Password=pass@word1");
        }
    }
}
