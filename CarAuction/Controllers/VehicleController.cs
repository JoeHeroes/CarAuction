using CarAuction.Models;
using CarAuction.Models.DTO;
using CarAuction.Models.Enum;
using CarAuction.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        [Route("VehicleSearch")]
        public IActionResult VehicleSearch(AuctionQuery query)
        {
            var baseQuery = dbContext.Vehicles.Where(x => x.RegistrationYear >= query.SinceYear && x.RegistrationYear <= query.ToYear); ;

            if(query.Producer != Producer.none)
            {
                var baseProducer = baseQuery.Where(x => x.Producer == query.Producer);
                baseQuery = baseProducer;
            }

            if (query.RegistrationYear != 0)
            {
                var baseRegistration = baseQuery.Where(x => x.RegistrationYear == query.RegistrationYear);
                baseQuery = baseRegistration;
            }

            if (query.LocationName != null)
            {
                var baseRegistration = baseQuery.Where(x => x.Location.Name == query.LocationName);
                baseQuery = baseRegistration;
            }

            /*

            if (query.Damage != null)
            {
                var baseRegistration = baseQuery.Where(x => x.Damage == query.Damage);
                baseQuery = baseRegistration;
            }

            */


            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Vehicle, object>>>
                {
                    { nameof(Vehicle.Producer), r => r.Producer },
                    { nameof(Vehicle.RegistrationYear), r => r.RegistrationYear },
                    { nameof(Vehicle.Location.Name), r => r.Location.Name },
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var vehicles = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();


            List<VehicleView> vehiclesView = new List<VehicleView>();

            foreach (var vehicle in vehicles)
            {
                VehicleView view = new VehicleView()
                {
                    LotNumber = vehicle.Id,
                    //Watch = vehicle.Bid.Watch,
                    RegistrationYear = vehicle.RegistrationYear,
                    Producer = vehicle.Producer,
                    ModelSpecifer = vehicle.ModelSpecifer,
                    //DateTime = vehicle.Sell.DateTime,
                    MeterReadout = vehicle.MeterReadout,
                    //Damage = vehicle.Sell.PrimaryDamage,
                    //CurrentBid = vehicle.Bid.CurrentBid
                };

                vehiclesView.Add(view);
            }

            return View(vehiclesView);
        }


        public IActionResult VehicleLot(int lotNumber)
        {
            Vehicle vehicle = this.dbContext.Vehicles.FirstOrDefault(x => x.Id == lotNumber);
            return View(vehicle);
        }
    }
}
