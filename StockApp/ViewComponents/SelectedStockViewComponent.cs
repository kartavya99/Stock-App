using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.FinnhubService;
using ServiceContracts.StocksService;

namespace StockApp.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubStockPriceQuoteSerivce _finnhubStockPriceQuoteSerivce;
        private readonly IBuyOrderService _stockService;        
        private readonly IConfiguration _configuration;

        public SelectedStockViewComponent(IOptions<TradingOptions> tradinOptions, IBuyOrderService stockService, IFinnhubCompanyProfileService finnhubCompanyProfileService, IFinnhubStockPriceQuoteSerivce finnhubStockPriceQuoteSerivce, IConfiguration configuration)
        {
            _tradingOptions = tradinOptions.Value;
            _stockService = stockService;
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubStockPriceQuoteSerivce = finnhubStockPriceQuoteSerivce;
            _configuration = configuration;
        }
            
        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            Dictionary<string, object>? companyProfileDict = null;

            if(stockSymbol != null)
            {
                companyProfileDict = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);
                var stockPriceDict = await _finnhubStockPriceQuoteSerivce.GetSotckPriceQuote(stockSymbol);
                if( stockPriceDict != null && companyProfileDict != null)
                {
                    companyProfileDict.Add("price", stockPriceDict["c"]);
                }               
            }            
            if (companyProfileDict != null && companyProfileDict.ContainsKey("logo"))
                return View(companyProfileDict);
            else
                return Content("");
        }
    }
}
