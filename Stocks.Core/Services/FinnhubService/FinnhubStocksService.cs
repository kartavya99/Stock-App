using Exceptions;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using ServiceContracts.FinnhubService;
using System.Text.Json;

namespace Services.FinnhubService
{
    public class FinnhubStocksService : IFinnhubSotckService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }
                   
        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            try
            {
                List<Dictionary<string, string>>? responseDictionary = await _finnhubRepository.GetStocks();
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



