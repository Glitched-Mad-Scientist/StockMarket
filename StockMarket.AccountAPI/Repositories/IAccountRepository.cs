using StockMarket.AccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.AccountAPI.Repositories
{
    public interface IAccountRepository
    {
        void AddUser(User item);
        public User CreateUser(string uname, string password, string email, string mobile);
        public User Validate(string uname, string pwd);
        public void UpdateUser(int UId, string uname, string password, string email, string mobile);
        public void ConfirmEmail(User user);
    }
}
