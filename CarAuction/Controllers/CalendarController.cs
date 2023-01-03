using CarAuction.Models;
using CarAuction.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Calendar")]
        public IActionResult Calendar()
        {
            ViewData["events"] = new[]
            {
                new EventViewModel { Id = 1, Title = "Video for Marisa", StartDate = "2023-01-14", BackgroundColor = "#8A2BE2", AllDay = true},
                new EventViewModel { Id = 2, Title = "Preparation", StartDate = "2023-01-12", BackgroundColor = "#A52A2A", AllDay = false},
            };

            return View();
        }
    }
}
