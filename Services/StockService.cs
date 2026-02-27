using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class StockService : IStocksService
    {
        private readonly StockMarketDbContext _db;

        public StockService(StockMarketDbContext stockmarketDbContext)
        {
            _db = stockmarketDbContext;
        }        

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            //Validation: buyOrederRequest can't be bull
            if (buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));

            //Model validation
            ValidationHelper.ModelValidation(buyOrderRequest);

            //Conver buyOrderRequest into BuyOrder type
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            //generate BuyOrder ID
            buyOrder.BuyOrderID = Guid.NewGuid();

            //add buy order object to buy orders list
            _db.BuyOrders.Add(buyOrder);
            await _db.SaveChangesAsync();

            //convert the BuyOrder object into BuyOrderResponse type
            return buyOrder.ToBuyOrderResponse();

        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            _db.SellOrders.Add(sellOrder);
            await _db.SaveChangesAsync();

            return sellOrder.ToSellOrderResponse();

        }
        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _db.BuyOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToListAsync();
            
            return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _db.SellOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToListAsync();
            
            return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        }       
    }
}