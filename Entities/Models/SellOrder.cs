using ServiceContracts.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SellOrder
    {
        [Key]
        public Guid SellOrderID { get; set; }

        [Required]
        [StringLength(10)]
        string StockSymbol { get; set; }
        [Required]
        [StringLength (40)]
        string StockName { get; set; }

        [OrderDateAtrributeCustomValidation]
        DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 
            0000)]
        uint Quantity { get; set; }
        [Range(1, 10000)]
        double Price { get; set; }
    }
}
