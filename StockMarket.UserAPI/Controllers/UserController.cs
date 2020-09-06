using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.UserAPI.Models;
using StockMarket.UserAPI.Services;
namespace StockMarket.UserAPI.Controllers
{

    [Route("api/User")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }
        [HttpGet]
        [Route("SearchCompany/{name}")]
        public IActionResult SearchCompany(string name)
        {
            try
            {
                Company company = service.SearchCompany(name);
                if(company==null)
                {
                    return Content("Invalid Company");
                }
                else
                {
                    return Ok(company);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("SearchStocksofCompany")]
        public IActionResult SearchStocksofCompany(Company company)
        {
            try
            {
                IEnumerable<StockPrice> stockPrices = service.SearchStocksofCompany(company);
                if (stockPrices == null)
                {
                    return Content("Invalid Company");
                }
                else
                {
                    return Ok(stockPrices);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("ComparePricesOfCompanies")]
        public IActionResult ComparePricesOfCompanies(DateTime dateTime)
        {
            try
            {
                IEnumerable<StockPrice> stockPrices = service.ComparePricesOfCompanies(dateTime);
                if (stockPrices == null)
                {
                    return Content("Invalid Date");
                }
                else
                {
                    return Ok(stockPrices);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
