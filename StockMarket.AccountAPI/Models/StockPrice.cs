using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.AccountAPI.Models
{
    [Table("StockPrice")]
    public class StockPrice
    {
        [Key]
        public int RowId { get; set; }
        [ForeignKey("Company")]
        [Required]
        public int CompanyCode { get; set; }
        [Required]
        [StringLength(30)]
        public string StockExchange { get; set; }
        [Required]
        public double CurrentPrice { get; set; }
        [Required]
        [Column(TypeName ="Date")]
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public Company Company { get; set; } //Navigation Prop
    }
}
