using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using StocksApp.Configuration;
using StocksApp.ViewModels;
using System.Reflection;
using System.Threading.Tasks;


namespace StocksApp.Controllers
{

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
            _tradingOptions = _options.Value;

        }

        [HttpGet("/")]
        [HttpGet("trade/index")]
        public IActionResult Index()
        {
            Dictionary<string, object>? stockPriceQuote = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol ?? "MSFT").Result ?? null;
            Dictionary<string, object>? companyProfile = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol ?? "MSFT").Result ?? null;

            StockTrade stockTradeViewModel = new()
            {
                StockSymbol = _tradingOptions.DefaultStockSymbol ?? "MSFT",
                StockName = (string?)companyProfile?["name"].ToString(),
                Price = Convert.ToDouble(stockPriceQuote?["c"].ToString())
            };

            return View(stockTradeViewModel);
        }

        [HttpGet("Trade/Orders")]
        public async Task<IActionResult> Orders()
        {
            List<BuyOrderResponse> buyOrders = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrders = await _stocksService.GetSellOrders();
            var ordersViewModel = new Orders();

            ordersViewModel.BuyOrders = buyOrders;
            ordersViewModel.SellOrders = sellOrders;

            return View(ordersViewModel);
        }



        [HttpPost("Trade/BuyOrder")]

        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrder)
        {
            // Fix CS8602: Check for null before accessing ModelState["DateAndTimeOfOrder"]
            if (!ModelState.IsValid && ModelState.TryGetValue("DateAndTimeOfOrder", out var dateState) && dateState is not null && dateState.Errors.Any())
            {
                ModelState.Clear(); // or remove specific error
                buyOrder.DateAndTimeOfOrder = DateTime.Now;
            }

            if (buyOrder == null || (!ModelState.IsValid)) return View("Orders");
            else
            {
                await _stocksService.CreateBuyOrder(buyOrder);
                return RedirectToAction("Orders", "Trade");
            }
        }


        [HttpPost("Trade/SellOrder")]

        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrder)
        {
            // Fix CS8602: Check for null before accessing ModelState["DateAndTimeOfOrder"]
            if (!ModelState.IsValid && ModelState.TryGetValue("DateAndTimeOfOrder", out var dateState) && dateState is not null && dateState.Errors.Any())
            {
                ModelState.Clear(); // or remove specific error
                sellOrder.DateAndTimeOfOrder = DateTime.Now;
            }

            if (sellOrder == null || (!ModelState.IsValid)) return View("Orders");
            else
            {
                await _stocksService.CreateSellOrder(sellOrder);
                return RedirectToAction("Orders", "Trade");
            }
        }

        [HttpGet("Trade/Explore")]
        public async Task<IActionResult> Explore()
        {
            List<string> stockList = _tradingOptions.Top25PopularStocksList ?? ["MSFT"];
            List<Stock> stockListViewModel = new();
            foreach (string stockName in stockList)
            {

                Dictionary<string, object>? companyProfile = _finnhubService.GetCompanyProfile(stockName).Result ?? null;

                Stock stockViewModel = new()
                {
                    StockSymbol = stockName,
                    StockName = (string?)companyProfile?["name"].ToString(),

                };

                stockListViewModel.Add(stockViewModel);
            }


            return View(stockListViewModel);
        }
    }
}
