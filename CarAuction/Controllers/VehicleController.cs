using CarAuction.Models;
using CarAuction.Models.DTO;
using CarAuction.Models.Enum;
using CarAuction.Models.View;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AutoAuction.Controllers
{
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

        [HttpPost]
        [Route("VehicleCreate")]
        public IActionResult VehicleCreate(CreateVehicleDto dto)
        {

            string stringFileName = UploadFile(dto);
            var vehicle = new Vehicle
            {
                Producer = dto.Producer,
                ModelSpecifer = dto.ModelSpecifer,
                ModelGeneration = dto.ModelGeneration,
                RegistrationYear = dto.RegistrationYear,
                Color = dto.Color,
                BodyType = dto.BodyType,
                Transmission = dto.Transmission,
                Drive = dto.Drive,
                MeterReadout = dto.MeterReadout,
                Fuel = dto.Fuel,
                PrimaryDamage = dto.PrimaryDamage,
                SecondaryDamage = dto.SecondaryDamage,
                VIN = dto.VIN,
                ProfileImg = stringFileName,
            };

            dbContext.Vehicles.Add(vehicle);
            dbContext.SaveChanges();
            return View(vehicle);
        }

        private string UploadFile(CreateVehicleDto dto)
        {
            string fileName = null;
            if(dto.PathPic!= null)
            {
                string uploadDir = Path.Combine(webHost.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + dto.PathPic.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dto.PathPic.CopyTo(fileStream);
                }

            }
            return fileName;
        }

        [HttpPost]
        [Route("Search")]
        public IActionResult Search(AuctionQuery query)
        {
            var baseQuery = dbContext.Vehicles.Where(x => x.RegistrationYear >= query.SinceYear && x.RegistrationYear <= query.ToYear);

            string searchPara = "";


            if (query.Producer != Producer.none)
            {
                var baseProducer = baseQuery.Where(x => x.Producer == query.Producer);
                baseQuery = baseProducer;
                searchPara += query.Producer.ToString();
            }

            if (query.SearchName != null)
            {
                var baseSearch = baseQuery.Where(r => query.SearchName == null 
                                                    || (r.ModelSpecifer.ToLower().Contains(query.SearchName.ToLower()) 
                                                    || r.VIN.ToLower().Contains(query.SearchName.ToLower())
                                                    || r.Id.ToString().Contains(query.SearchName)));

                if (baseSearch != null)
                {
                    baseQuery = baseSearch;
                    searchPara += query.SearchName;
                }
            }


            if (query.LocationName != null)
            {
                var baseLocation = baseQuery.Where(x => x.Location.Name == query.LocationName);
                baseQuery = baseLocation;
                searchPara += ", " + query.LocationName.ToString();
            }

            if (query.RegistrationYear != 0)
            {
                var baseRegistration = baseQuery.Where(x => x.RegistrationYear == query.RegistrationYear);
                baseQuery = baseRegistration;
                searchPara += ", " + query.RegistrationYear.ToString();
            }

            if (query.Damage != Damage.none)
            {
                var baseDamage = baseQuery.Where(x => x.PrimaryDamage == query.Damage);
                baseQuery = baseDamage;
                searchPara += ", " + query.Damage.ToString();
            }

            HttpContext.Session.SetString("searchBy", searchPara);

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
                    ProfileImg= vehicle.ProfileImg,
                    CurrentBid = vehicle.CurrentBid
                };

                vehiclesView.Add(view);
            }
            return View(vehiclesView);
        }

        public IActionResult Lot(int lotNumber)
        {
            Vehicle vehicle = this.dbContext
                                    .Vehicles
                                    .FirstOrDefault(x => x.Id == lotNumber);

            return View(vehicle);
        }

        [HttpPost]
        public IActionResult UpdateBid(int lotNumber, int bidNow)
        {
            Vehicle vehicle = this.dbContext
                                .Vehicles
                                .FirstOrDefault(x => x.Id == lotNumber);

            if(bidNow > vehicle.CurrentBid)
            {
                vehicle.BidStatus = true;
                vehicle.CurrentBid = bidNow;
                dbContext.SaveChanges();
                TempData["Success"] = "You bid is higher now";
            }
            else
            {
                TempData["Error"] = "You bid must be higher that current";
            }

            return RedirectToAction("Lot", new { @lotNumber = lotNumber });
        }
    }
}
