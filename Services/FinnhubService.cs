using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IConfiguration _configuration;


        public FinnhubService(IConfiguration configuration)
        {
            _configuration = configuration;
        }   


        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            Dictionary<string, object>? result = new();
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["token"]}");
                string? content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
            }

             return result;
        }
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            Dictionary<string, object>? result = new();
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["token"]}");
                string? content = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
            }

            return result;

        }
    }
}
