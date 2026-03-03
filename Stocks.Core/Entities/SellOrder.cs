using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Represents a sell order to sell the stocks
    /// </summary>
    public class SellOrder
    {
        [Key]
        public Guid SellOrderID { get; set; }

        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 10000, ErrorMessage = "You can buy maximum of 10000 share in single order. Mimimum is 1.")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage = "THe maximum price of stock is 10000. Mimimun is 1.")]
        public double Price { get; set; }
    }
}
