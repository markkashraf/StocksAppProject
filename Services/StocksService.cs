using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly StocksDbContext _db;



        public StocksService(StocksDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException();

            if(buyOrderRequest.Quantity < 1 || buyOrderRequest.Quantity > 100000) throw new ArgumentException();

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

           await _db.buyOrders.AddAsync(result.ToBuyOrder());
           await _db.SaveChangesAsync();          
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


            await _db.sellOrders.AddAsync(result.ToSellOrder());
            await _db.SaveChangesAsync();


            return result;

        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrderResponse> result = new List<BuyOrderResponse>();
            foreach(var item in _db.buyOrders.ToList())
            {
                result.Add(ConvertBuyOrderToRespnose(item));
            }

            return result;
        }




        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrderResponse> result = new List<SellOrderResponse>();
            foreach (var item in _db.sellOrders.ToList())
            {
                result.Add(ConvertSellOrderToRespnose(item));
            }

            return result;
        }

        private BuyOrderResponse ConvertBuyOrderToRespnose(BuyOrder input)
        {
            return new BuyOrderResponse
            {
                BuyOrderID = input.BuyOrderID,
                DateAndTimeOfOrder = input.DateAndTimeOfOrder,
                Price = input.Price,
                Quantity = input.Quantity,
                StockName = input.StockName,
                StockSymbol = input.StockSymbol,
                TradeAmount = 0
            };
        }



        private SellOrderResponse ConvertSellOrderToRespnose(SellOrder input)
        {
            return new SellOrderResponse
            {
                SellOrderID = input.SellOrderID,
                DateAndTimeOfOrder = input.DateAndTimeOfOrder,
                Price = input.Price,
                Quantity = input.Quantity,
                StockName = input.StockName,
                StockSymbol = input.StockSymbol,
                TradeAmount = 0
            };
        }

    }
}


