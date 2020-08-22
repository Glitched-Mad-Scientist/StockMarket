using StockMarket.AdminAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.AdminAPI.DBAccess;
using Microsoft.AspNetCore.Server.IIS.Core;
using StockMarket.AccountAPI.Repositories;

namespace StockMarket.AdminAPI.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private StockDBContext context;
        public AdminRepository(StockDBContext context)
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

        public void UpdateCompany(int CId, string sector = null, string cname = null, long turnover = 0, string ceo = null, string bod = null, string se = null, string sc = null, string desc = null)
        {
            Company company = context.Companies.Find(CId);
            if (cname != null)
                company.CompanyName = cname;
            if (ceo != null)
                company.CEO = ceo;
            if (bod != null)
                company.BoardofDirectors = bod;
            if (se != null)
                company.StockExchanges = se;
            if (sc != null)
                company.StockCodes = sc;
            if (turnover != 0)
                company.Turnover = turnover;
            if (sector != null)
                company.Sector = sector;
            if (desc != null)
                company.Description = desc;
            context.Update(company);
        }

        public void DeleteCompany(Company item)
        {
            context.Companies.Remove(item);
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
