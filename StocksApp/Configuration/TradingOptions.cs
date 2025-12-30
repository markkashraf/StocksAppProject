namespace StocksApp.Configuration
{
    public class TradingOptions
    {
        public string? DefaultStockSymbol { get; set; }
        public int DefaultOrderQuantity {get; set;}
        public string Top25PopularStocks {get; set;} = "MSFT";
        public List<string>? Top25PopularStocksList{ get=> [.. Top25PopularStocks.Split(',')]; }
        
    }
}
