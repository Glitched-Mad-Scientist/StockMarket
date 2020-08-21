using StockMarket.AccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.AccountAPI.DBAccess;
using Microsoft.AspNetCore.Server.IIS.Core;
using StockMarket.AccountAPI.Infastructure;

namespace StockMarket.AccountAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private StockDBContext context;
        private encryption enc;
        public AccountRepository(StockDBContext context)
        {
            this.context = context;
        }
        public void AddUser(User item)
        {
            context.Add(item);
            context.SaveChanges();
        }
        public User CreateUser(string uname,string password,string email=null,string mobile=null,string confirmed= null)
        {
            User user = new User();
            user.Username = uname;
            string hashed = enc.Encrypt(password);
            user.Password = hashed;
            user.Email = email;
            user.Mobile = mobile;
            user.Confirmed = confirmed;
            return user;
        }

        public void UpdateUser(int UId, string uname=null, string password=null, string email = null, string mobile = null, string confirmed = null)
        {
            User user = context.Users.Find(UId);
            if(uname != null)
                user.Username = uname;
            if (password != null)
                user.Password = enc.Encrypt(password);
            if (email != null)
                user.Email = email;
            if (mobile != null)
                user.Mobile = mobile;
            if (confirmed != null)
                user.Confirmed = confirmed;
            context.Update(user);
            context.SaveChanges();
        }

        public User Validate(string uname, string pwd)
        {
            User user = context.Users.SingleOrDefault(i => i.Username == uname && i.Password == enc.Encrypt(pwd));
            return user;
        }
    }
}
