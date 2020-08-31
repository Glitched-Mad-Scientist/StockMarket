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
        public User CreateUser(string uname, string password, string email, string mobile);
        User Validate(string uname, string pwd);
        public void UpdateUser(int userId, string uname, string password, string email, string mobile);
        public void ConfirmEmail(User user);
        public bool isTaken(string username);
        public void ConfirmationEmail(string url, string Email);
    }
}
