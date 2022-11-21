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


        public IActionResult Place(int locationId)
        {
            Location location = this.dbContext.Locations.FirstOrDefault(x => x.Id == locationId);
            return View(location);
        }


    }
}
