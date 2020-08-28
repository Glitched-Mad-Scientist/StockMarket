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
        public AccountRepository(StockDBContext context)
        {
            this.context = context;
        }
        public void AddUser(User item)
        {
            context.Add(item);
            context.SaveChanges();
        }
        public User CreateUser(string uname,string password,string email=null,string mobile=null)
        {
            User user = new User();
            user.Username = uname;
            string hashed = encryption.Encrypt(password,uname);
            user.Password = hashed;
            user.Email = email;
            user.Mobile = mobile;
            user.Confirmed = "No";
            return user;
        }

        public void UpdateUser(int UId, string uname=null, string password=null, string email = null, string mobile = null)
        {
            User user = context.Users.Find(UId);
            if(uname != null)
                user.Username = uname;
            if (password != null)
                user.Password = encryption.Encrypt(password,uname);
            if (email != null)
            {
                user.Email = email;
                user.Confirmed = "No";
            }
            if (mobile != null)
                user.Mobile = mobile;
            context.Update(user);
            context.SaveChanges();
        }

        public User Validate(string uname, string pwd)
        {
            User user = context.Users.SingleOrDefault(i => i.Username == uname);
            if (encryption.Decrypt(user.Password, uname) == pwd)
                return user;
            return null;
        }

        public void ConfirmEmail(User user)
        {
            user.Confirmed = "Yes";
            context.Update(user);
            context.SaveChanges();
        }
    }
}
