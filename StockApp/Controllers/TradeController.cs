using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using StockApp.Models;
using System.Collections.Generic;

namespace StockApp.Controlers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;
        private readonly IConfiguration _configuration;

        public TradeController(IOptions<TradingOptions> tradingOptions, IStocksService stockService, IFinnhubService finnhubService, IConfiguration configuration )
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
            _stocksService = stockService;
            _configuration = configuration;
        }


        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public async Task<IActionResult> Index()
        {
            //reset stock symbol if not exists
            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
                _tradingOptions.DefaultStockSymbol = "MSFT";

            //get company profile from API server
            Dictionary<string, object>? companyProfileDictionary = await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);

            //get stock price quote from API server
            Dictionary<string, object>? stockQuoteDictionary = await _finnhubService.GetSotckPriceQuote(_tradingOptions.DefaultStockSymbol);

            //create model object
            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };

            //load data from finnHubService into model object
            if(companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                stockTrade = new StockTrade()
                {
                    StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]),
                    StockName = Convert.ToString(companyProfileDictionary["name"]),
                    Quantity = _tradingOptions.DefaultOrderQuantity ?? 0,
                    Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString()),                    
                };
            }

            //Send Finnhub token to view
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            //update date of order
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            //re-validate the model object after updating the date
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = buyOrderRequest.StockName, Quantity = buyOrderRequest.Quantity, StockSymbol = buyOrderRequest.StockSymbol };
                return View("Index", stockTrade);
            }

            //invoke servie method
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            return RedirectToAction(nameof(Orders));

        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            //update date of order
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            //re-validate the model object after updating the date
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = sellOrderRequest.StockName, Quantity = sellOrderRequest.Quantity, StockSymbol = sellOrderRequest.StockSymbol };
                return View("Index", stockTrade);
            }

            //invoke servie method
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

            return RedirectToAction(nameof(Orders));

        }

        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {
            //invoke service methods
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();

            // crate model object
            Orders order = new Orders() { BuyOrders = buyOrderResponses, SellOrders = sellOrderResponses };

            ViewBag.TradingOptions = _tradingOptions;

            return View(order);
        }

        [Route("OrdersPDF")]
        public async Task<IActionResult> OrdersPDF()
        {
            List<IOrderResponse> orders = new List<IOrderResponse>();
            orders.AddRange(await _stocksService.GetBuyOrders());
            orders.AddRange(await _stocksService.GetSellOrders());
            orders = orders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToList();

            ViewBag.TradingOptions = _tradingOptions;

            return new ViewAsPdf("OrdersPDF", orders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
    }
}
