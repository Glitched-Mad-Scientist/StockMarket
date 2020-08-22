using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.AdminAPI.Repositories;
using StockMarket.AdminAPI.Models;
using StockMarket.AccountAPI.Repositories;

namespace StockMarket.AdminAPI.Services
{
    public class AdminService:IAdminService
    {
        private IAdminRepository adminRepository;
        public AdminService(IAdminRepository repo)
        {
            adminRepository = repo;
        }

        public void AddCompany(Company item)
        {
            adminRepository.AddCompany(item);
        }
        public Company CreateCompany(string sector, string cname, long turnover = 0, string ceo = null, string bod = null, string se = null, string sc = null, string desc = null)
        {
            return adminRepository.CreateCompany(sector,cname,turnover,ceo,bod,se,sc,desc);
        }

        public void UpdateCompany(int CId, string sector = null, string cname = null, long turnover = 0, string ceo = null, string bod = null, string se = null, string sc = null, string desc = null)
        {
            adminRepository.UpdateCompany(CId,sector,cname,turnover,ceo,bod,se,sc,desc);
        }

        public void DeleteCompany(Company item)
        {
            adminRepository.DeleteCompany(item);
        }

        public Company ValidateName(string cname)
        {
            return adminRepository.ValidateName(cname);
        }

        public Company ValidateCid(int cid)
        {
            return adminRepository.ValidateCid(cid);
        }
    }
}
