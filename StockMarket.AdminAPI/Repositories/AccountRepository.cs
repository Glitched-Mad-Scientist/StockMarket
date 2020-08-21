using StockMarket.AdminAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.AdminAPI.DBAccess;
using Microsoft.AspNetCore.Server.IIS.Core;
using StockMarket.AdminAPI.Models;

namespace StockMarket.AccountAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private StockDBContext context;
        public AccountRepository(StockDBContext context)
        {
            this.context = context;
        }
        public void AddCompany(User item)
        {
            context.Add(item);
            context.SaveChanges();
        }
        public Company CreateCompany(string uname,string password,string email=null,string mobile=null,string confirmed= null)
        {
            User user = new User();
            user.Username = uname;
            user.Password = password;
            user.Email = email;
            user.Mobile = mobile;
            user.Confirmed = confirmed;
            return user;
        }

        public void UpdateUser(int? UId, string uname=null, string password=null, string email = null, string mobile = null, string confirmed = null)
        {
            User user = context.Users.Find(UId);
            if(uname != null)
                user.Username = uname;
            if (password != null)
                user.Password = password;
            if (email != null)
                user.Email = email;
            if (mobile != null)
                user.Mobile = mobile;
            if (confirmed != null)
                user.Confirmed = confirmed;
            context.Update(user);
        }

        public User Validate(string uname, string pwd)
        {
            User user = context.Users.SingleOrDefault(i => i.Username == uname && i.Password == pwd);
            return user;
        }
    }
}
