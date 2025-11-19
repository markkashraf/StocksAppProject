using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get => field = Price * Quantity; set; }

    }

    public static class SellOrderResponseExtension
    {
        public static SellOrder ToSellOrder(this SellOrderResponse order)
        {
            SellOrder result = new SellOrder() { SellOrderID = order.SellOrderID, DateAndTimeOfOrder = order.DateAndTimeOfOrder, Price = order.Price, Quantity = order.Quantity, StockName = order.StockName, StockSymbol = order.StockSymbol };
            return result;
        }
    }
}
