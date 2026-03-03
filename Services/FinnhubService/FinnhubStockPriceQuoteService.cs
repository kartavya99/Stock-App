using Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.FinnhubService;
using System.Text.Json;

namespace Services.FinnhubService
{
    public class FinnhunStockPriceQuoteService : IFinnhubStockPriceQuoteSerivce
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhunStockPriceQuoteService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> GetSotckPriceQuote(string stockSymbol)
        {
            try
            {
                Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetSotckPriceQuote(stockSymbol);
                return responseDictionary;
            } 
            catch (Exception ex)
            {
                FinnhubException finnhubExeption = new FinnhubException("Unable to connect to finnhub", ex);
                throw finnhubExeption;
            }
        }        
    }
}



