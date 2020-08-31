using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.AccountAPI.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password should be between 5 and 30 characters")]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string Mobile { get; set; }

    }
}
