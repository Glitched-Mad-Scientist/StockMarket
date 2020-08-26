using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Xml;
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
                if(user.Confirmed == "No")
                {
                    return Content("Email has not been confirmed yet.");
                }
                return Ok(user);
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
                User item = service.CreateUser(uname,password,email,mobile);
                service.AddUser(item);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(string uname, string password, string email, string mobile, User user)
        {
            try
            {
                service.UpdateUser(user.UserId, uname, password, email, mobile);
                if (email != null)
                    service.ConfirmEmail(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("ConfirmationEmail")]
        public IActionResult CofirmationEmail(User user)
        {
            try
            {
                var callbackUrl = Url.Action("ConfirmEmail", "Account", user, protocol: HttpContext.Request.Scheme);
                String url = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("teertharajchatterjee@gmail.com");
                msg.To.Add(user.Email);
                msg.Subject = "This is your confirmation email";
                msg.Body = url;
                msg.IsBodyHtml = true;

                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp.gmail.com";
                System.Net.NetworkCredential ntwd = new NetworkCredential();
                ntwd.UserName = "teertharajchatterjee@gmail.com";
                ntwd.Password = "";
                smt.UseDefaultCredentials = true;
                smt.Credentials = ntwd;
                smt.Port = 587;
                smt.EnableSsl = true;
                smt.Send(msg);
                return Ok("Confirmation email has been sent.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("ConfirmEmail")]
        public IActionResult CofirmEmail(User user)
        {
            try
            {
                service.ConfirmEmail(user);
                return Ok("Email Confirmed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
