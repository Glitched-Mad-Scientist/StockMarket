using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.AccountAPI.Models
{
    [Table("IPO")]
    public class IPO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string CompanyName { get; set; }
        [ForeignKey("Company")]
        [Required]
        public int CompanyCode { get; set; }
        [Required]
        [StringLength(30)]
        public string StockExchange { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime OpenDate { get; set; }
        [Required]
        public double PricePerShare { get; set; }
        [Required]
        public int TotalNumberOfShares { get; set; }
        [StringLength(150)]
        public string Remarks { get; set; }
        public Company Company { get; set; } //Navigation Prop
    }
}
