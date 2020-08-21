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

    [Route("api/[controller]")]
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
        public IActionResult AddUser(User item)
        {
            try
            {
                service.AddUser(item);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
