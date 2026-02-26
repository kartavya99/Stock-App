using ServiceContracts.DTO;

namespace StockApp.Models
{
    /// <summary>
    /// Represents model class to supply list of buy orders and sell orders to the Trades/Orders view
    /// </summary>
    public class Orders
    {
        public List<BuyOrderResponse> buyOrders { get; set; } = new List<BuyOrderResponse>();

        public List<SellOrderResponse> sellOrders { get; set; } = new List<SellOrderResponse>();
    }
}
