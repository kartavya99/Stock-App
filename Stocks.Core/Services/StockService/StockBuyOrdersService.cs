using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.StocksService;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.StockService
{
    public class StockBuyOrdersService : IBuyOrderService
    {
        private readonly IStockRepository _stockRepostiory;

        public StockBuyOrdersService(IStockRepository stockRepostiory)
        {
            _stockRepostiory = stockRepostiory;
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
            BuyOrder buyOrderFromRepo = await _stockRepostiory.CreateBuyOrder(buyOrder);

            //convert the BuyOrder object into BuyOrderResponse type
            return buyOrder.ToBuyOrderResponse();

        }
        
        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _stockRepostiory.GetBuyOrders();
            
            return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
        }
                
    }
}