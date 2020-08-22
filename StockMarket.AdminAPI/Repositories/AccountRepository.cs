using StockMarket.AdminAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.AdminAPI.DBAccess;
using Microsoft.AspNetCore.Server.IIS.Core;
using StockMarket.AdminAPI.Models;

namespace StockMarket.AdminAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private StockDBContext context;
        public AccountRepository(StockDBContext context)
        {
            this.context = context;
        }
        public void AddCompany(Company item)
        {
            context.Add(item);
            context.SaveChanges();
        }
        public Company CreateCompany(string sector, string cname, long turnover=0,string ceo=null,string bod=null,string se=null,string sc= null, string desc = null)
        {
            Company company = new Company();
            company.CompanyName = cname;
            company.CEO = ceo;
            company.BoardofDirectors = bod;
            company.StockExchanges = se;
            company.StockCodes = sc;
            company.Turnover = turnover;
            company.Sector = sector;
            company.Description = desc;
            return company;
        }

        public void UpdateCompany(int? UId, string uname=null, string password=null, string email = null, string mobile = null, string confirmed = null)
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

        public Company ValidateName(string cname)
        {
            Company company = context.Companies.SingleOrDefault(i => i.CompanyName == cname);
            return company;
        }

        public Company ValidateCid(int cid)
        {
            Company company = context.Companies.SingleOrDefault(i => i.CompanyCode == cid);
            return company;
        }

    }
}
