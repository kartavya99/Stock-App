using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.FinnhubService;
using ServiceContracts.StocksService;
using StockApp.Filters.ActionFilters;
using StockApp.Models;
using System.Collections.Generic;


namespace StockApp.Controlers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubSotckService _finnhubSotckService;
        private readonly IFinnhubStockPriceQuoteSerivce _finnhubStockPriceQuoteSerivce;
        private readonly IBuyOrderService _buyOrderService;
        private readonly ISellOrderService _sellOrderService;
        private readonly IConfiguration _configuration;

        public TradeController(IOptions<TradingOptions> tradingOptions, IBuyOrderService buyOrderService, ISellOrderService sellOrderService, IFinnhubCompanyProfileService finnhubCompanyProfileService, IFinnhubSotckService finnhubSotckService, IFinnhubStockPriceQuoteSerivce finnhubStockPriceQuoteSerivce, IConfiguration configuration )
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubSotckService = finnhubSotckService;
            _finnhubStockPriceQuoteSerivce = finnhubStockPriceQuoteSerivce;
            _buyOrderService = buyOrderService;
            _sellOrderService = sellOrderService;
            _configuration = configuration;
        }


        [Route("[action]/{stockSymbol}")]
        [Route("~/[controller]/{stockSymbol}")]
        public async Task<IActionResult> Index(string stockSymbol)
        {
            //reset stock symbol if not exists
            if (string.IsNullOrEmpty(stockSymbol))
                stockSymbol = "MSFT";

            //get company profile from API server
            Dictionary<string, object>? companyProfileDictionary = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);

            //get stock price quote from API server
            Dictionary<string, object>? stockQuoteDictionary = await _finnhubStockPriceQuoteSerivce.GetSotckPriceQuote(stockSymbol);

            //create model object
            StockTrade stockTrade = new StockTrade() { StockSymbol = stockSymbol };

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
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest orderRequest)
        {
            //invoke servie method
            BuyOrderResponse buyOrderResponse = await _buyOrderService.CreateBuyOrder(orderRequest);

            return RedirectToAction(nameof(Orders));

        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> SellOrder(SellOrderRequest orderRequest)
        {
            //invoke servie method
            SellOrderResponse sellOrderResponse = await _sellOrderService.CreateSellOrder(orderRequest);

            return RedirectToAction(nameof(Orders));

        }

        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {
            //invoke service methods
            List<BuyOrderResponse> buyOrderResponses = await _buyOrderService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = await _sellOrderService.GetSellOrders();

            // crate model object
            Orders order = new Orders() { BuyOrders = buyOrderResponses, SellOrders = sellOrderResponses };

            ViewBag.TradingOptions = _tradingOptions;

            return View(order);
        }

        [Route("OrdersPDF")]
        public async Task<IActionResult> OrdersPDF()
        {
            List<IOrderResponse> orders = new List<IOrderResponse>();
            orders.AddRange(await _buyOrderService.GetBuyOrders());
            orders.AddRange(await _sellOrderService.GetSellOrders());
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
