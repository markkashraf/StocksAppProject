

using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit.Sdk;
using EntityFrameworkCoreMock;
using Moq;

namespace StocksAppTest
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;


        public StocksServiceTest()
        {
            var stocksBuyOrdersInitalData = new List<BuyOrder>() { };
            var stocksSellOrdersInitalData = new List<BuyOrder>() { };

            StocksDbContext dbContext = new 
            _stocksService = new StocksService();


        }



        #region CreateBuyOrderTests

        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestisNull_ThrowsArgumentNullException()
        {
            BuyOrderRequest? buyOrderRequest = null;
            await Assert.ThrowsAsync<ArgumentNullException>(() => _stocksService.CreateBuyOrder(buyOrderRequest));
        }
        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestQuantityEqualsZero_ThrowsArgumentException()
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest
            {
                Quantity = 0
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(buyOrderRequest));
        }
        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestQuantityEquals100001_ThrowsArgumentException()
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest
            {
                Quantity = 100001
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(buyOrderRequest));
        }
        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestPriceEqualsZero_ThrowsArgumentException()
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest
            {
                Price = 0
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(buyOrderRequest));
        }
        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestPriceEquals10001_ThrowsArgumentException()
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest
            {
                Price = 0
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(buyOrderRequest));
        }
        [Fact]
        public async Task CreateBuyOrder_StockSymbolisNull_ThrowsArgumentNullException()
        {
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = null };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(buyOrderRequest));
        }
        [Fact]
        public async Task CreateBuyOrder_dateAndTimeOfOrderBefore2000_ThrowsArgumentNullException()
        {
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest() { DateAndTimeOfOrder = DateTime.Parse("1999-12-31") };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(buyOrderRequest));
        }

        [Fact]
        public async Task CreateBuyOrder_Correct()
        {
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 500, Quantity = 40, DateAndTimeOfOrder = DateTime.Parse("2005-12-31") };
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
            Assert.NotNull(buyOrderResponse);

        }
        #endregion

        #region CreateSellOrderTests
        [Fact]
        public async Task CreateSellOrder_SellOrderRequestisNull_ThrowsArgumentNullException()
        {
            SellOrderRequest? sellOrderRequest = null;
            await Assert.ThrowsAsync<ArgumentNullException>(() => _stocksService.CreateSellOrder(sellOrderRequest));
        }
        [Fact]
        public async Task CreateSellOrder_SellOrderRequestQuantityEqualsZero_ThrowsArgumentException()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest
            {
                Quantity = 0
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(sellOrderRequest));
        }
        [Fact]
        public async Task CreateSellOrder_SellOrderRequestQuantityEquals100001_ThrowsArgumentException()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest
            {
                Quantity = 100001
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(sellOrderRequest));
        }
        [Fact]
        public async Task CreateSellOrder_SellOrderRequestPriceEqualsZero_ThrowsArgumentException()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest
            {
                Price = 0
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(sellOrderRequest));
        }
        [Fact]
        public async Task CreateSellOrder_SellOrderRequestPriceEquals10001_ThrowsArgumentException()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest
            {
                Price = 0
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(sellOrderRequest));
        }
        [Fact]
        public async Task CreateSellOrder_StockSymbolisNull_ThrowsArgumentNullException()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest() { StockSymbol = null };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(sellOrderRequest));
        }
        [Fact]
        public async Task CreateSellOrder_dateAndTimeOfOrderBefore2000_ThrowsArgumentNullException()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest() { DateAndTimeOfOrder = DateTime.Parse("1999-12-31") };
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(sellOrderRequest));
        }

        [Fact]
        public async Task CreateSellOrder_Correct()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest() { StockName = "Microsoft", StockSymbol = "MSFT", Price = 500, Quantity = 40, DateAndTimeOfOrder = DateTime.Parse("2005-12-31") };
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);
            Assert.NotNull(sellOrderResponse);

        }
        #endregion



        #region GetAllBuyOdersTests
        [Fact]
        public async Task GetBuyOrders_EmptyList_ReturnsEmptyList()
        {

            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();

            Assert.Empty(buyOrderResponses);
        }
        [Fact]
        public async Task GetBuyOrders_FilledList_ReturnsFilledList()
        {
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 500, Quantity = 40, DateAndTimeOfOrder = DateTime.Parse("2005-12-31") };
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();
            Assert.NotEmpty(buyOrderResponses);

        }

        #endregion

        #region GetAllSellOrdersTests
        #endregion
    }
}