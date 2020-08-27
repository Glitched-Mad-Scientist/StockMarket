using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockMarket.AccountAPI.Models;
using StockMarket.AccountAPI.Services;

namespace StockMarket.AccountAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private IAccountService service;
        private readonly IConfiguration configuration;
        public AccountController(IAccountService service,IConfiguration configuration)
        {
            this.service = service;
            this.configuration = configuration;
        }
        [HttpGet]
        [Route("Validate/{uname}/{pwd}")]
        [AllowAnonymous]
        public IActionResult Validate(string uname,string pwd)
        {
            try
            {
                if(uname == "Admin" && pwd == "12345")
                    return Ok(GenerateJwtToken(uname, "Admin"));
                User user = service.Validate(uname, pwd);
                if(user==null)
                {
                    return Content("Invalid User");
                }
                if(user.Confirmed == "No")
                {
                    return Content("Email has not been confirmed yet.");
                }
                return Ok(GenerateJwtToken(uname,"User"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("AddUser")]
        [AllowAnonymous]
        public IActionResult AddUser(User user)
        {
            try
            {
                User item = service.CreateUser(user.Username,user.Password,user.Email,user.Mobile);
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
        public IActionResult UpdateUser(User update, User user)
        {
            try
            {
                service.UpdateUser(user.UserId, update.Username, update.Password, update.Email, update.Mobile);
                if (update.Email != null)
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

        private string GenerateJwtToken(string uname, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, uname),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, uname),
                new Claim(ClaimTypes.Role,role)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
