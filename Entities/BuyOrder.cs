using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Represents a buy order to purchase the stocks
    /// </summary>
    public class BuyOrder
    {
        [Key]
        public Guid BuyOrderId { get; set; }

        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 10000, ErrorMessage ="You can buy maximum of 10000 share in single order. Mimimum is 1.")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage ="THe maximum price of stock is 10000. Mimimun is 1.")]
        public double Price {  get; set; }                              

    }
}
