using CarAuction.Models;
using Microsoft.AspNetCore.Mvc;

namespace UniAPI.Controllers
{

    public class LocationController: Controller
    {

        private readonly AuctionDbContext dbContext;
       

        public LocationController(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult GetAll()
        {
            List<Location> locations = this.dbContext.Locations.ToList();
            return View(locations);
        }


        public IActionResult Espoo()
        {
            Location location = this.dbContext.Locations.FirstOrDefault(x => x.City == "Espoo");
            return View(location);
        }

        public IActionResult Oulu()
        {
            Location location = this.dbContext.Locations.FirstOrDefault(x => x.City == "Oulu");
            return View(location);
        }

        public IActionResult Pirkkala()
        {
            Location location = this.dbContext.Locations.FirstOrDefault(x => x.City == "Pirkkala");
            return View(location);
        }

        public IActionResult Turku()
        {
            Location location = this.dbContext.Locations.FirstOrDefault(x => x.City == "Turku");
            return View(location);
        }

    }
}
