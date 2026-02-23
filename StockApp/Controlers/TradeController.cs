using Microsoft.AspNetCore.Mvc;

namespace StockApp.Controlers
{
    public class TradeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
