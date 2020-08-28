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
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private IAdminService service;
        public AdminController(IAdminService service)
        {
            this.service = service;
        }
        [HttpPost]
        [Route("Validate/{cname}")]
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
        [Route("AddCompany")]
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
        [Route("UpdateCompany")]
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
        [Route("DeleteCompany")]
        public IActionResult DeactivateCompany(Company company)
        {
            try
            {
                Company item = service.ValidateCid(company.CompanyCode);
                service.DeleteCompany(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
