using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CustomValidations;
namespace Entities.Models
{
    public class BuyOrder
    {
    [Key]
    public Guid BuyOrderID { get; set; }

    [Required]
    string StockSymbol { get; set; }
    [Required]
    string StockName { get; set; }

    [OrderDateAtrributeCustomValidation]
    DateTime DateAndTimeOfOrder { get; set; }

    [Range(1, 100000)]
    uint Quantity { get; set; }
    [Range(1 ,10000)]
    double Price { get; set; }
    }
}
