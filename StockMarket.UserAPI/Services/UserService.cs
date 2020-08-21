using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.UserAPI.Repositories;
using StockMarket.UserAPI.Models;
namespace StockMarket.UserAPI.Services
{
    public class UserService:IUserService
    {
        private IUserRepository userRepository;
        public UserService(IUserRepository repo)
        {
            userRepository = repo;    
        }

        public Company SearchCompany(string name) => userRepository.SearchCompany(name);
    }
}
