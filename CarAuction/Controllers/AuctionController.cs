using CarAuction.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Controllers
{
    public class AuctionController : Controller
    {
        private readonly AuctionDbContext dbContext;

        public AuctionController(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Actual()
        {
            var auction = this.dbContext.Vehicles.ToList();
            return View(auction);
        }
    }
}
