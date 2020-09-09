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

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        // GET: api/user/companies
        [HttpGet]
        [Route("Companies/All")]
        public IActionResult GetCompanies()
        {
            try
            {
                List<Company> companies = service.GetCompanies();
                if (companies.Any()) return Ok(companies);
                return NotFound("No active company found in record.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/user/companies/AIR
        [HttpGet]
        [Route("Companies/{id}")]
        public IActionResult GetCompany(int id)
        {
            try
            {
                Company company = service.GetCompanyByCompanyCode(id);
                if (company != null) return Ok(company);
                return NotFound(id + " not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/user/companies/AIR
        [HttpGet]
        [Route("Companies/Search/{query}")]
        public IActionResult GetCompanies(string query)
        {
            try
            {
                List<Company> companies = service.GetCompanies(query);
                if (companies.Any()) return Ok(companies);
                return NotFound("No active company found matching " + query);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("StockPrices/{companyCode}/{startDate}/{endDate}")]
        public IActionResult GetStockPrices(int companyCode, DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate.Date > endDate.Date)
                    return BadRequest("the 'From' date should be before the 'To' date");

                //if (!service.IsActive(companyCode)) 
                //    return NotFound("No active company identified by " + companyCode);
                Company _company = service.GetCompanyByCompanyCode(companyCode);
                List<StockPrice> _stockPrices = service.GetCompanyDetails(companyCode, startDate, endDate);
                return Ok(
                    new { company = _company, stockPrices = _stockPrices }
                );

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
