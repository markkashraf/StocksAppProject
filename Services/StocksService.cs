using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /*
CreateBuyOrder: Inserts a new buy order into the database table called 'BuyOrders'.

CreateSellOrder: Inserts a new sell order into the database table called 'SellOrders'.

GetBuyOrders: Returns the existing list of buy orders retrieved from database table called 'BuyOrders'.

GetSellOrders: Returns the existing list of sell orders retrieved from database table called 'SellOrders'.

*/
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrderResponse>? _buyOrdersDB;
        private readonly List<SellOrderResponse>? _sellOrdersDB;




        public StocksService()
        {
            _buyOrdersDB = new List<BuyOrderResponse>();
            _sellOrdersDB = new List<SellOrderResponse>();
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException();

            if(buyOrderRequest.Quantity < 1 || buyOrderRequest.Quantity > 
                
                
                0000) throw new ArgumentException();

            if(buyOrderRequest.Price > 10000 ||  buyOrderRequest.Price < 1) throw new ArgumentException();

            if(buyOrderRequest.StockSymbol is null) throw new ArgumentException();

            if(buyOrderRequest.DateAndTimeOfOrder <= DateTime.Parse("2000-01-01")) throw new ArgumentException();

            var result = new BuyOrderResponse
            {
                BuyOrderID = new(),
                Price = buyOrderRequest.Price,
                DateAndTimeOfOrder = buyOrderRequest.DateAndTimeOfOrder,
                StockName = buyOrderRequest.StockName,
                StockSymbol = buyOrderRequest.StockSymbol,
                Quantity = buyOrderRequest.Quantity
            };

            _buyOrdersDB?.Add(result);
            return result;


        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null) throw new ArgumentNullException();

            if (sellOrderRequest.Quantity < 1 || sellOrderRequest.Quantity > 100000) throw new ArgumentException();

            if (sellOrderRequest.Price > 10000 || sellOrderRequest.Price < 1) throw new ArgumentException();

            if (sellOrderRequest.StockSymbol is null) throw new ArgumentException();

            if (sellOrderRequest.DateAndTimeOfOrder <= DateTime.Parse("2000-01-01")) throw new ArgumentException();

            var result = new SellOrderResponse
            {
                SellOrderID = new(),
                Price = sellOrderRequest.Price,
                DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
                StockName = sellOrderRequest.StockName,
                StockSymbol = sellOrderRequest.StockSymbol,
                Quantity = sellOrderRequest.Quantity
            };

            _sellOrdersDB?.Add(result);

            return result;

        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return _buyOrdersDB;
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            return _sellOrdersDB;
        }
    }
}
