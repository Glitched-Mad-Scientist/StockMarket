using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.AdminAPI.Models;
using StockMarket.AdminAPI.Services;
namespace StockMarket.AdminAPI.Controllers
{

    [Route("api/Admin")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private IAdminService service;
        public AdminController(IAdminService service)
        {
            this.service = service;
        }
        [HttpPost]
        [Route("Company/Validate/{cname}")]
        public IActionResult Validate(string cname)
        {
            try
            {
                Company company = service.ValidateName(cname);
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
        [Route("Company/Add")]
        public IActionResult AddCompany(Company company)
        {
            try
            {
                Company item = service.CreateCompany(company.Sector, company.CompanyName, company.Turnover, company.CEO, company.BoardofDirectors, company.StockExchanges, company.StockCodes, company.Description);
                service.AddCompany(item);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        [Route("Company/Update")]
        public IActionResult UpdateCompany(Company update)
        {
            try
            {
                service.UpdateCompany(update.CompanyCode, update.Sector, update.CompanyName, update.Turnover, update.CEO, update.BoardofDirectors, update.StockExchanges, update.StockCodes, update.Description);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        [Route("Company/Delete/{companyCode}")]
        public IActionResult DeleteCompany(int companyCode)
        {
            try
            {
                Company item = service.ValidateCid(companyCode);
                service.DeleteCompany(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("Company/All")]
        public IActionResult GetAllCompanies()
        {
            try
            {
                List<Company> companies = service.GetCompanies();
                if (companies.Any())
                    return Ok(companies);
                return NotFound("No companies in record.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("IPO/All")]
        public IActionResult GetAllIPOs()
        {
            try
            {
                List<IPO> iPOs = service.GetIPOs();
                if (iPOs.Any())
                    return Ok(iPOs);
                return NotFound("No IPOs in record.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("IPO/Add")]
        public IActionResult AddIPO(IPO iPODetails)
        {
            try
            {
                return Ok(service.AddIPO(iPODetails));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("IPO/Update")]
        public IActionResult UpdateIPO(IPO iPODetails)
        {
            try
            {
                return Ok(service.UpdateIPO(iPODetails));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("StockPrices/Missing/{companyCode}/{startDate}/{endDate}")]
        public IActionResult GetMissingStockPriceDates(int companyCode, DateTime startDate, DateTime endDate)
        {
            try
            {
                List<DateTime> missingDates = service.GetMissingStockPriceDates(companyCode, startDate, endDate);
                if (missingDates.Any()) return Ok(missingDates);
                return NotFound($"no missing dates found between {startDate} and {endDate}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("Company/Activate/{companyCode}")]
        public IActionResult ActivateCompany(int companyCode)
        {
            try
            {
                return Ok(service.ActivateCompany(companyCode));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("Company/Deactivate/{companyCode}")]
        public IActionResult DeactivateCompany(int companyCode)
        {
            try
            {
                return Ok(service.DeactivateCompany(companyCode));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
