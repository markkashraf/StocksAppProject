using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StocksApp.Configuration;
using Entities;

namespace StocksApp.Controllers
{

    public class TradeController : Controller
    {
        private readonly IOptions<TradingOptions> _options;
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        

        public TradeController(IOptions<TradingOptions> options, IFinnhubService finnhubService)
        {
            _options = options;
            _tradingOptions = options.Value;
            _finnhubService = finnhubService;

        }
        
        [Route("/")]
        public IActionResult Index()
        {

            Dictionary<string,object>? stockPriceQuote = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol ?? "MSFT").Result ?? null;
            Dictionary<string,object>? companyProfile = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol?? "MSFT").Result ?? null;

            StockTrade stockTradeViewModel = new()
            {
                StockSymbol = _tradingOptions.DefaultStockSymbol ?? "MSFT",
                StockName = (string?) companyProfile["name"].ToString(),
                Quantity = 5,
                Price = Convert.ToDouble(stockPriceQuote["c"].ToString())
            };

             


            return View(stockTradeViewModel);
        }
    }
}
