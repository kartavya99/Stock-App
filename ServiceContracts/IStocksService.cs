using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts
{
    public interface IStocksService
    {
        /// <summary>
        /// Creates a buy order
        /// </summary>
        /// <param name="buyOrderResponse">Buy order object</param>
        /// <returns>Returns the Buy order object including newly genereated buy order id</returns>
        BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

        /// <summary>
        /// Creates a sell order
        /// </summary>
        /// <param name="sellOrderRequest">Sell order object</param>
        /// <returns>Returns the Sell order object including newly generated sell order id</returns>
        SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest);

        /// <summary>
        /// Return all existing buy orders
        /// </summary>
        /// <returns>Returns a list of objects of BuyOrder type</returns>
        List<BuyOrderResponse> GetBuyOrders();

        /// <summary>
        /// Returns all existing sell orders
        /// </summary>
        /// <returns>Returns a list of objects of SellOrder type</returns>
        List<SellOrderResponse> GetSellOrders();
    }
}
