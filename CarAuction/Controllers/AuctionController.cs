using CarAuction.Models;
using CarAuction.Models.Enum;
using CarAuction.Models.View;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public IActionResult Today()
        {
            var auction = dbContext.Vehicles.Where(x => x.DateTime == DateTime.Now);

            List<VehicleView> vehiclesView = new List<VehicleView>();

            foreach (var vehicle in auction)
            {
                VehicleView view = new VehicleView()
                {
                    LotNumber = vehicle.Id,
                    RegistrationYear = vehicle.RegistrationYear,
                    Producer = vehicle.Producer,
                    ModelSpecifer = vehicle.ModelSpecifer,
                    DateTime = vehicle.DateTime,
                    MeterReadout = vehicle.MeterReadout,
                    Damage = vehicle.PrimaryDamage,
                    ProfileImg = vehicle.ProfileImg,
                    Watch = vehicle.Watch,
                    CurrentBid = vehicle.CurrentBid,
                    WinnerId = vehicle.WinnerId,
                };

                vehiclesView.Add(view);
            }
            return View(vehiclesView);
        }
    }
}
