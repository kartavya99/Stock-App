using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryContracts
{
    public interface IStockRepository
    {
        /// <summary>
        /// Creates a buy order
        /// </summary>
        /// <param name="buyOrder">Buy order object</param>        
        /// <returns>Buy Order object</returns>
        Task<BuyOrder> CreateOrder(BuyOrder buyOrder);

        /// <summary>
        /// Creates a sell order
        /// </summary>
        /// <param name="sellOrder">Sell order object</param>
        /// <returns>Sell Order object</returns>
        Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

        /// <summary>
        /// Returns all existing buy order
        /// </summary>
        /// <returns>Returns a list of objects of BuyOrder type</returns>
        Task<List<BuyOrder>> GetBuyOrders();

        /// <summary>
        /// Returns all exisitng sell order
        /// </summary>
        /// <returns></returns>
        Task<List<SellOrder>> GetSellOrders();
    }
}
