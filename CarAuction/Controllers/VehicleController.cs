using CarAuction.Models;
using CarAuction.Models.DTO;
using CarAuction.Models.View;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AutoAuction.Controllers
{
    [Route("Vehicle")]
    public class VehicleController : Controller
    {
        private readonly AuctionDbContext dbContext;

        public VehicleController(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [Route("Finder")]
        public IActionResult Finder()
        {
            return View();
        }

        [Route("WatchList")]
        public IActionResult WatchList()
        {
            return View();
        }



        [HttpPost]
        [Route("Search")]
        public IActionResult VehicleSearch(AuctionQuery query)
        {
            // Page control
            query.PageSize = 2;
            query.PageNumber = 2;


            var vehicles = this.dbContext.Vehicles
                                            .Where(r => query.SearchPhrase == null || 
                                            (r.Producer.ToString().ToLower().Contains(query.SearchPhrase.ToLower())|| 
                                             r.Location.ToString().ToLower().Contains(query.SearchPhrase.ToLower())))
                                            .Skip(query.PageSize * (query.PageNumber - 1))
                                            .Take(query.PageSize)
                                            .ToList( );

            List<VehicleView> vehiclesView = new List<VehicleView>();

            foreach (InfoVehicle vehicle in vehicles)
            {
                VehicleView view = new VehicleView()
                {
                    LotNumber = vehicle.LotNumber,
                    //Watch = vehicle.InfoBid.Watch,
                    RegistrationYear = vehicle.RegistrationYear,
                    Producer = vehicle.Producer,
                    ModelSpecifer = vehicle.ModelSpecifer,
                    //DateTime = vehicle.InfoSell.DateTime,
                    MeterReadout = vehicle.MeterReadout,
                    //Damage = vehicle.InfoSell.PrimaryDamage,
                    //CurrentBid = vehicle.InfoBid.CurrentBid
                };

                vehiclesView.Add(view);
            }

            return View(vehiclesView);
        }
    }
}
