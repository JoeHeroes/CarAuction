using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
