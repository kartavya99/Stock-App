using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.StocksService
{
    public interface ISellOrderService
    {
       
        /// <summary>
        /// Creates a sell order
        /// </summary>
        /// <param name="sellOrderRequest">Sell order object</param>
        /// <returns>Returns the Sell order object including newly generated sell order id</returns>
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

        /// <summary>
        /// Returns all existing sell orders
        /// </summary>
        /// <returns>Returns a list of objects of SellOrder type</returns>
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
