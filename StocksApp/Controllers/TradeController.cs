using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using StocksApp.Configuration;
using StocksApp.ViewModels;
using System.Reflection;


namespace StocksApp.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IOptions<TradingOptions> _options;
        private TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;
        

        public TradeController(IOptions<TradingOptions> options, IFinnhubService finnhubService, IStocksService stocksService)
        {
            _options = options;
            _finnhubService = finnhubService;
            _stocksService = stocksService;

        }
        
        [HttpGet("/")]
        [HttpGet("[action]")]
        public IActionResult Index()
        {
            _tradingOptions = _options.Value;
            Dictionary<string,object>? stockPriceQuote = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol ?? "MSFT").Result ?? null;
            Dictionary<string,object>? companyProfile = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol?? "MSFT").Result ?? null;

            StockTrade stockTradeViewModel = new()
            {
                StockSymbol = _tradingOptions.DefaultStockSymbol ?? "MSFT",
                StockName = (string?) companyProfile?["name"].ToString(),
                Price = Convert.ToDouble(stockPriceQuote?["c"].ToString())
            };

            return View(stockTradeViewModel);
        }

        [HttpGet("[action]")]
        public IActionResult Orders()
        {
            var buyOrders = _stocksService.GetBuyOrders();
            var sellOrders = _stocksService.GetSellOrders();
            var ordersViewModel = new Orders();
            ordersViewModel.BuyOrders = buyOrders.Result;
            ordersViewModel.SellOrders = sellOrders.Result;
         
            return View(ordersViewModel);
        }



        [HttpPost("[action]")]

        public IActionResult BuyOrder(BuyOrderRequest buyOrder)
        {

            if (!ModelState.IsValid && ModelState["DateAndTimeOfOrder"].Errors.Any())
            {
                ModelState.Clear(); // or remove specific error
                buyOrder.DateAndTimeOfOrder = DateTime.Now;
            }


            if (buyOrder == null || (!ModelState.IsValid) ) return View("Orders");
            else
            {
                _stocksService.CreateBuyOrder(buyOrder);
                return RedirectToAction("Orders", "Trade");
            }    
        }


        [HttpPost("[action]")]

        public IActionResult SellOrder(SellOrderRequest sellOrder)
        {

            if (!ModelState.IsValid && ModelState["DateAndTimeOfOrder"].Errors.Any())
            {
                ModelState.Clear(); // or remove specific error
                sellOrder.DateAndTimeOfOrder = DateTime.Now; 
            }

            if (sellOrder == null || (!ModelState.IsValid)) return View("Orders");
            else
            {
                _stocksService.CreateSellOrder(sellOrder);
                return RedirectToAction("Orders", "Trade");
            }
        }
    }
}
