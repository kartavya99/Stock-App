using Exceptions;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using ServiceContracts.FinnhubService;
using System.Text.Json;

namespace Services.FinnhubService
{
    public class FinnhubSearchStocksService : IFinnhubSearchStocksService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubSearchStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }      

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            try
            {
                Dictionary<string, object>? responseDictionary = await _finnhubRepository.SearchStocks(stockSymbolToSearch);
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



