using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.ExcelAPI.Models
{
    [Table("Company")]
    public class Company
    {
        [Key]
        public int CompanyCode { get; set; }
        [Required]
        [StringLength(25)]
        public string CompanyName { get; set; }
        [StringLength(30)]
        public string CEO { get; set; }
        [StringLength(100)]
        public string BoardofDirectors { get; set; }
        public long Turnover { get; set; }
        [StringLength(30)]
        public string StockExchanges { get; set; }
        [StringLength(30)]
        public string StockCodes { get; set; }
        [Required]
        [StringLength(30)]
        public string Sector { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public IEnumerable<StockPrice> StockPrices { get; set; } //Navigation Prop
        public IEnumerable<IPO> IPOs { get; set; } //Navigation Prop
    }
}
