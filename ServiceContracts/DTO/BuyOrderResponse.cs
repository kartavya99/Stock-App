using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that represents a buy order to purchase the stocks - that can be used as return type of Stock service
    /// </summary>
    public class BuyOrderResponse
    {    
        public Guid BuyOrderID { get; set; }
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string StockName {  get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        /// <summary>
        /// Checks if the current object and other (parameter) object values match
        /// </summary>
        /// <param name="obj">Other object of BuyOrderResponse class, to compare</param>
        /// <returns>True or false determins whether current object and other objects match</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not BuyOrderResponse) return false;

            BuyOrderResponse other = (BuyOrderResponse)obj;
            return BuyOrderID == other.BuyOrderID && StockSymbol == other.StockSymbol && StockName == other.StockName &&
                DateAndTimeOfOrder == other.DateAndTimeOfOrder && Quantity == other.Quantity && Price == other.Price;
        }

        /// <summary>
        /// Returns an int value that represents unique stock id of the current object
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return StockSymbol.GetHashCode();
        }

        public override string ToString()
        {
            return $"Buy Order ID: {BuyOrderID}, Stock Symbol: {StockSymbol}, Date and Time of Buy Order: {DateAndTimeOfOrder.ToString("dd MM yyyy hh:mm ss tt")}, Quantity: {Quantity}, Buy Price: {Price}, Trade Amount: {TradeAmount}";
        }
    }

    public static class BuyOrderExtension
    {
        /// <summary>
        /// An extension method to convert an object of BuyOrder class into BuyOrderResponse class
        /// </summary>
        /// <param name="buyOrder"></param>
        /// <returns></returns>
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse()
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                Price = buyOrder.Price,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                TradeAmount = buyOrder.Price * buyOrder.Quantity
            };
        }
    }
}
