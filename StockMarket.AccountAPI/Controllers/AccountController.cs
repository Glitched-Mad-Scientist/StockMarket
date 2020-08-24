using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.AccountAPI.Models;
using StockMarket.AccountAPI.Services;
namespace StockMarket.AccountAPI.Controllers
{

    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService service;
        public AccountController(IAccountService service)
        {
            this.service = service;
        }
        [HttpGet]
        [Route("Validate/{uname}/{pwd}")]
        public IActionResult Validate(string uname,string pwd)
        {
            try
            {
                User user = service.Validate(uname, pwd);
                if(user==null)
                {
                    return Content("Invalid User");
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(string uname, string password, string email, string mobile, string confirmed)
        {
            try
            {
                User item = service.CreateUser(uname,password,email,mobile,confirmed);
                service.AddUser(item);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(string uname, string password, string email, string mobile, string confirmed,User user)
        {
            try
            {
                service.UpdateUser(user.UserId, uname, password, email, mobile, confirmed);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
