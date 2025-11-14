using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Configuration;

namespace StocksApp.Controllers
{

    public class TradeController : Controller
    {
        private readonly IOptions<TradingOptions> _options;
        private readonly TradingOptions _tradingOptions;

        public TradeController(IOptions<TradingOptions> options)
        {
            _options = options;
            _tradingOptions = options.Value;
        }
        
        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Test = _tradingOptions.DefaultStockSymbol;
            return View();
        }
    }
}
