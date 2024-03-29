﻿using CarAuction.Models;
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
