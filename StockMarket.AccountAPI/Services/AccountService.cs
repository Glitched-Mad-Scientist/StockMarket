using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.AccountAPI.Repositories;
using StockMarket.AccountAPI.Models;
namespace StockMarket.AccountAPI.Services
{
    public class AccountService:IAccountService
    {
        private IAccountRepository accountRepository;
        public AccountService(IAccountRepository repo)
        {
            accountRepository = repo;
        }

        public void AddUser(User item)
        {
            accountRepository.AddUser(item);
        }
        public User CreateUser(string uname, string password, string email, string mobile)
        {
            return accountRepository.CreateUser(uname,password,email,mobile);
        }
        public User Validate(string uname, string pwd)
        {
            return accountRepository.Validate(uname, pwd);
        }
        public void UpdateUser(int UId, string uname, string password, string email, string mobile)
        {
            accountRepository.UpdateUser(UId,uname,password,email,mobile);        
        }
    }
}
