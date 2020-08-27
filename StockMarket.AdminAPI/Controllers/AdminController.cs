using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [Route("Validate/{cname}")]
        public IActionResult Validate(string cname)
        {
            try
            {
                Company company = service.ValidateName(cname);
                if(company==null)
                {
                    return Content("Invalid User");
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
        public IActionResult AddCompany(string sector, string cname, long turnover, string ceo, string bod, string se, string sc, string desc)
        {
            try
            {
                Company item = service.CreateCompany(sector, cname, turnover, ceo, bod, se, sc, desc);
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
        public IActionResult UpdateCompany(int CId, string sector, string cname, long turnover, string ceo, string bod, string se, string sc, string desc)
        {
            try
            {
                service.UpdateCompany(CId, sector, cname, turnover, ceo, bod, se, sc, desc);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteCompany")]
        public IActionResult DeactivateCompany(int CId)
        {
            try
            {
                Company item = service.ValidateCid(CId);
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
