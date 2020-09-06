using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockMarket.AccountAPI.DTOs;
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
        [HttpPost]
        [Route("Validate")]
        [AllowAnonymous]
        public IActionResult Validate(LoginUserDTO loginUser)
        {
            try
            {
                User user = service.Validate(loginUser.Username, loginUser.Password);
                if(user==null)
                {
                    return BadRequest("Invalid User");
                }
                if(user.Confirmed == "No")
                {
                    return BadRequest("Email has not been confirmed yet.");
                }
                return Ok(new { token = GenerateJwtToken(user) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("AddUser")]
        [AllowAnonymous]
        public IActionResult AddUser(RegisterUserDTO registerUserDTO)
        {
            try
            {
                User item = service.CreateUser(registerUserDTO.Username,registerUserDTO.Password,registerUserDTO.Email,registerUserDTO.Mobile);
                service.AddUser(item);
                return Ok(registerUserDTO);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(User update)
        {
            try
            {
                if (update.Password == "0")
                    service.UpdateUser(update.UserId, update.Username, null, update.Email, update.Mobile);
                else
                    service.UpdateUser(update.UserId, update.Username, update.Password, update.Email, update.Mobile);

                /*if (update.Email != null)
                {
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", update, protocol: HttpContext.Request.Scheme);
                    String url = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";

                    service.ConfirmationEmail(url, update.Email);
                    return Ok("Profile Updated & Confirmation email sent.");
                }*/
                return Ok(update);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
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

                service.ConfirmationEmail(url,user.Email);
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

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim("Id", user.UserId.ToString()),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("mobilephone",user.Mobile)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["Jwt:ExpireDays"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Issuer"]
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        [HttpGet]
        [AllowAnonymous]
        [Route("isTaken/{username}")]
        public IActionResult isTaken(string username)
        {
            try
            {
                return Ok(service.isTaken(username));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
