using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that represents a sell order - that can be used as return type of Stocks service
    /// </summary>
    public class SellOrderResponse : IOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }

        public OrderType TypeOfOrder => OrderType.SellOrder;
        public double TradeAmount { get; set; }

        /// <summary>
        /// Checks if the current object and other (parameter) object values match
        /// </summary>
        /// <param name="obj">Other object of SellOrderResponse class, to compare</param>
        /// <returns>True or false determines whether current object and other objects match</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not SellOrderResponse) return false;

            SellOrderResponse other = (SellOrderResponse)obj;
            return SellOrderID == other.SellOrderID && StockSymbol == other.StockSymbol && StockName == other.StockName 
                && DateAndTimeOfOrder == other.DateAndTimeOfOrder && Quantity == other.Quantity && Price == other.Price;
        }

        /// <summary>
        /// Returns an int value that represents unqiue id of the current object
        /// </summary>
        /// <returns>unique int value</returns>
        public override int GetHashCode()
        {
            return StockSymbol.GetHashCode();
        }

        public override string ToString()
        {
            return $"Sell Order ID: {SellOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, Date and Time of Sell Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm ss tt")}, Quantity: {Quantity}, Sell Price: {Price}, Trade Amount: {TradeAmount}";
        }
    }

    public static class SellOrderExtensions
    {
        /// <summary>
        /// An extension method to convert an object of SellOrder class into SellOrderResponse class
        /// </summary>
        /// <param name="sellOrder">The SellOrder object to convert</param>
        /// <returns>Returns the converted SellOrderResponse object</returns>
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse() 
            { 
                SellOrderID = sellOrder.SellOrderID, 
                StockSymbol = sellOrder.StockSymbol, 
                StockName = sellOrder.StockName, 
                Price = sellOrder.Price, 
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder, 
                Quantity = sellOrder.Quantity, 
                TradeAmount = sellOrder.Price * sellOrder.Quantity 
            };
        }
    }
}
