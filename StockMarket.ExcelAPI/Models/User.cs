using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.ExcelAPI.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [StringLength(30)]
        public string Role { get; set; }
        [Required]
        [StringLength(1000)]
        public string Password { get; set; }
        [EmailAddress]
        [StringLength(30)]
        public string Email { get; set; }
        [StringLength(30)]
        public string Mobile { get; set; }
        [StringLength(30)]
        public string Confirmed { get; set; }
    }
}
