using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Controllers
{
    public class CalendarController : Controller
    {
        [Route("Calendar")]
        public IActionResult Calendar()
        {
            return View();
        }
    }
}
