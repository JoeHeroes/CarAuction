using CarAuction.Models;
using CarAuction.Models.View;
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
        [Route("Actual")]
        public IActionResult Actual()
        {
            var auction = dbContext.Vehicles.Where(x => x.DateTime <= DateTime.Now && x.DateTime.AddHours(1) >= DateTime.Now);
            
            var lol = auction.ToList();
            
            if (auction.Any() == true)
            {
                return View(auction);
            }

            return View("NoLive");
        }

        [Route("NoLive")]
        public IActionResult NoLive()
        {
            return View();
        }


        [Route("Today")]
        public IActionResult Today()
        {
            var auction = dbContext.Vehicles.Where(x => x.DateTime.Date == DateTime.Now.Date);

            List<VehicleView> vehiclesView = new List<VehicleView>();

            foreach (var vehicle in auction)
            {
                var watchResult = this.dbContext.Watches.Where(x => x.VehicleId == vehicle.Id);
                var result = watchResult.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("id")));

                bool watchBool = false;

                if (result != null)
                {
                    watchBool = true;
                }


                var restultPictures = this.dbContext.Pictures.Where(x => x.Id == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach(var pic in restultPictures)
                {
                    pictures.Add(pic.PathImg);
                }


                VehicleView view = new VehicleView()
                {
                    LotNumber = vehicle.Id,
                    RegistrationYear = vehicle.RegistrationYear,
                    Producer = vehicle.Producer,
                    ModelSpecifer = vehicle.ModelSpecifer,
                    DateTime = vehicle.DateTime,
                    MeterReadout = vehicle.MeterReadout,
                    Damage = vehicle.PrimaryDamage,
                    Image = pictures[0],
                    Watch = watchBool,
                    CurrentBid = vehicle.CurrentBid,
                    WinnerId = vehicle.WinnerId,
                };

                vehiclesView.Add(view);
            }
            return View(vehiclesView);
        }
    }
}
