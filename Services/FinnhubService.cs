using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
    public class FinnhubService : IfinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration


        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public Dictionary<string, object>? GetCompanyProfile(string stockSymbol)
        {
            //Create http client
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //Create http request
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
            };

            //Send request
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

            //read response body
            string responseBody = new StreamReader(httpRequestMessage.Content.ReadAsStream()).ReadToEnd();

            // convert response boyd (from JSON into Dictionary)
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            // return response dictionary back to the caller
            return responseDictionary;               
            
        }

        public Dictionary<string, object>? GetSotckPriceQuote(string stockSymbol)
        {
            //Create http client
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //Create http request
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
            };

            //Send request
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

            //read response body
            string responseBody = new StreamReader(httpRequestMessage.Content.ReadAsStream()).ReadToEnd();

            // convert response boyd (from JSON into Dictionary)
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

            // return response dictionary back to the caller
            return responseDictionary;
        }
    }
}



