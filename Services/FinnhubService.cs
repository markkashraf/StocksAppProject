using ServiceContracts;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        public Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            //using(var client = new HttpClient())
            //{
            //    var response = client.GetAsync($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={token}");
            //}
            throw new NotImplementedException();

        }

        public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            throw new NotImplementedException();
        }
    }
}
