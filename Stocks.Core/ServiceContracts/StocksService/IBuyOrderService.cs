using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.StocksService
{
    public interface IBuyOrderService
    {
        /// <summary>
        /// Creates a buy order
        /// </summary>
        /// <param name="buyOrderResponse">Buy order object</param>
        /// <returns>Returns the Buy order object including newly genereated buy order id</returns>
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

        /// <summary>
        /// Return all existing buy orders
        /// </summary>
        /// <returns>Returns a list of objects of BuyOrder type</returns>
        Task<List<BuyOrderResponse>> GetBuyOrders();

     }
}
