using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockApp.Models;

namespace StockApp.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {

        private readonly IFinnhubService _fiinhubService;
        private readonly TradingOptions _tradingOptions;

        /// <summary>
        /// Constructor for TradeController that excutes when a new object is created for the class
        /// </summary>
        /// <param name="tradingOptions">Injecting TradeOptions config through Options pattern</param>
        /// <param name="finnhubService">Injecting FinnhubService</param>
        public StocksController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService)
        {
            _tradingOptions = tradingOptions.Value;
            _fiinhubService = finnhubService;
        }


        [Route("/")]
        [Route("[action]/{stock?}")]
        [Route("~/[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            //get company profile from API server
            List<Dictionary<string, string>>? stocksDictionary = await _fiinhubService.GetStocks();

            List<Stock> stocks = new List<Stock>();

            if(stocksDictionary is not null)
            {
                //fiter stocks
                if(!showAll && _tradingOptions.Top25PopularStocks != null)
                {
                    string[]? Top25PopularStocksList = _tradingOptions.Top25PopularStocks.Split(",");
                    if (Top25PopularStocksList is not null)
                    {
                        stocksDictionary = stocksDictionary
                            .Where(temp => Top25PopularStocksList.Contains(Convert.ToString(temp["symbol"])))
                            .ToList();
                    }
                }

                //covert dictionary objects into Stock objects
                stocks = stocksDictionary
                    .Select(temp => new Stock() 
                    { 
                        StockName = Convert.ToString(temp["description"]), 
                        StockSymbol = Convert.ToString(temp["symbol"]) })
                    .ToList();
            }

            ViewBag.stock = stock;
            return View(stocks);
        }
    }
}
