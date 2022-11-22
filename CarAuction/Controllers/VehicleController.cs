using CarAuction.Models;
using CarAuction.Models.Enum;
using CarAuction.Models.View;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AutoAuction.Controllers
{
    [Route("Vehicle")]
    public class VehicleController : Controller
    {
        private readonly AuctionDbContext dbContext;

        private readonly IWebHostEnvironment webHost;

        public VehicleController(AuctionDbContext dbContext, IWebHostEnvironment webHost)
        {
            this.dbContext = dbContext;
            this.webHost = webHost;
        }


        [Route("Finder")]
        public IActionResult Finder()
        {
            return View();
        }


        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        //Fix

        [HttpPost]
        public IActionResult VehicleCreate(VehicleView vv)
        {

            string stringFileName = UploadFile(vv);
            var vehicle = new Vehicle
            {

                ProfileImg = stringFileName,
                RegistrationYear = vv.RegistrationYear,
                Producer = vv.Producer,
                ModelSpecifer = vv.ModelSpecifer,

            };

            dbContext.Vehicles.Add(vehicle);
            dbContext.SaveChanges();
            return View();
        }

        private string UploadFile(VehicleView vv)
        {
            string fileName = null;
            if(vv.PathPic!= null)
            {
                string uploadDir = Path.Combine(webHost.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vv.PathPic.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vv.PathPic.CopyTo(fileStream);
                }

            }
            return fileName;
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
                var baseLocation = baseQuery.Where(x => x.Location.Name == query.LocationName);
                baseQuery = baseLocation;
            }


            if (query.Damage != Damage.none)
            {
                var baseDamage = baseQuery.Where(x => x.PrimaryDamage == query.Damage);
                baseQuery = baseDamage;
            }




            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Vehicle, object>>>
                {
                    { nameof(Vehicle.Id), r => r.Id },
                    { nameof(Vehicle.Producer), r => r.Producer },
                    { nameof(Vehicle.RegistrationYear), r => r.RegistrationYear },
                    { nameof(Vehicle.Location.Name), r => r.Location.Name },
                    { nameof(Vehicle.PrimaryDamage), r => r.PrimaryDamage },
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
                    Watch = vehicle.Watch,
                    RegistrationYear = vehicle.RegistrationYear,
                    Producer = vehicle.Producer,
                    ModelSpecifer = vehicle.ModelSpecifer,
                    DateTime = vehicle.DateTime,
                    MeterReadout = vehicle.MeterReadout,
                    Damage = vehicle.PrimaryDamage,
                    CurrentBid = vehicle.CurrentBid
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
