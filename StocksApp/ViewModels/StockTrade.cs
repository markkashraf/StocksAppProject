using ServiceContracts.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace StocksApp.ViewModels
{
    public class StockTrade
    {
        [Required]
        public string? StockSymbol { get; set; }
        [Required]
        public string? StockName { get; set; }

        [Range(1, 
            
            0000)]
        public uint Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }


    }

}
