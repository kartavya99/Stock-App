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
    public class StockSellOrdersService : ISellOrderService
    {
        private readonly IStockRepository _stockRepostiory;

        public StockSellOrdersService(IStockRepository stockRepostiory)
        {
            _stockRepostiory = stockRepostiory;
        }        

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            SellOrder sellOrderFromRepo = await _stockRepostiory.CreateSellOrder(sellOrder);

            return sellOrder.ToSellOrderResponse();

        }       

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _stockRepostiory.GetSellOrders();
            
            return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        }       
    }
}