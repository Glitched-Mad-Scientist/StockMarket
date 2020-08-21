using StockMarket.AccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.AccountAPI.Services
{
   public interface IAccountService
    {
        void AddUser(User item);
        User Validate(string uname, string pwd);
    }
}
