using CarAuction.Models;
using CarAuction.Models.DTO;
using CarAuction.Models.Enum;
using CarAuction.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarAuction.Controllers
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

        public IActionResult Finder()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult WatchList()
        {
            return View();
        }


        [HttpPost]
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
                DateTime= dto.DateTime,
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

            List<string> searchPara = new List<string>();


            if (query.Producer != Producer.none)
            {
                var baseProducer = baseQuery.Where(x => x.Producer == query.Producer);
                baseQuery = baseProducer;
                searchPara.Add(query.Producer.ToString());
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
                    searchPara.Add(query.SearchName);
                }
            }


            if (query.LocationName != null)
            {
                var baseLocation = baseQuery.Where(x => x.Location.Name == query.LocationName);
                baseQuery = baseLocation;
                searchPara.Add(query.LocationName.ToString());
            }

            if (query.RegistrationYear != 0)
            {
                var baseRegistration = baseQuery.Where(x => x.RegistrationYear == query.RegistrationYear);
                baseQuery = baseRegistration;
                searchPara.Add(query.RegistrationYear.ToString());
            }

            if (query.Damage != Damage.none)
            {
                var baseDamage = baseQuery.Where(x => x.PrimaryDamage == query.Damage);
                baseQuery = baseDamage;
                searchPara.Add(query.Damage.ToString());
            }

            string stringSearch = "";


            for(int i = 0; i<searchPara.Count(); i++)
            {
                if (i == 0)
                {
                    stringSearch += searchPara[i];
                }
                else
                {
                    stringSearch += ", " + searchPara[i];
                }
            }


            HttpContext.Session.SetString("searchBy", stringSearch);

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
                    RegistrationYear = vehicle.RegistrationYear,
                    Producer = vehicle.Producer,
                    ModelSpecifer = vehicle.ModelSpecifer,
                    DateTime = vehicle.DateTime,
                    MeterReadout = vehicle.MeterReadout,
                    Damage = vehicle.PrimaryDamage,
                    ProfileImg= vehicle.ProfileImg,
                    Watch = vehicle.Watch,
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

            int id = int.Parse(HttpContext.Session.GetString("id"));

            if (bidNow > vehicle.CurrentBid)
            {
                vehicle.WinnerId = id;
                vehicle.CurrentBid = bidNow;

                User user = this.dbContext
                                  .Users
                                  .FirstOrDefault(x => x.Id == id);

                var bind = new CurrentBind()
                {
                    UserMany = user,
                    VehicleMany = vehicle,
                };

                this.dbContext.CurrentBinds.Add(bind);


                dbContext.SaveChanges();
                TempData["Success"] = "You bid is higher now";
            }
            else
            {
                TempData["Error"] = "You bid must be higher that current";
            }

            return RedirectToAction("Lot", new { @lotNumber = lotNumber });
        }

        public IActionResult Watch(int lotNumber)
        {

            int id = int.Parse(HttpContext.Session.GetString("id"));

            User user = this.dbContext
                                    .Users
                                    .FirstOrDefault(x => x.Id == id);

            Vehicle vehicle = this.dbContext
                                    .Vehicles
                                    .FirstOrDefault(x => x.Id == lotNumber);

            var observed = new Watch()
            {
                UserMany = user,
                VehicleMany = vehicle,
            };

            this.dbContext.Watches.Add(observed);

            vehicle.Watch = true;


            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }


            return RedirectToAction("Lot", new { @lotNumber = lotNumber });
        }

        public IActionResult RemoveWatch(int lotNumber)
        {
            int id = int.Parse(HttpContext.Session.GetString("id"));

            Vehicle vehicle = this.dbContext
                                    .Vehicles
                                    .FirstOrDefault(x => x.Id == lotNumber);

            Watch observed = this.dbContext.Watches.FirstOrDefault(x => x.UserId == id && x.VehicleId == vehicle.Id);

            this.dbContext.Watches.Remove(observed);


            vehicle.Watch = false;

            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return RedirectToAction("Lot", new { @lotNumber = lotNumber });
        }


        [Route("Watchlist")]
        public IActionResult Watchlist()
        {
            int id = int.Parse(HttpContext.Session.GetString("id"));


            var watch = this.dbContext.Watches.Where(x => x.UserId == id);

            List<Vehicle> vehicles = new List<Vehicle>();

            var vehiclesList = this.dbContext.Vehicles.ToList();

            foreach (var x in watch)
            {
                var dish = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId);
                vehicles.Add(dish);
            }


            List<VehicleView> vehiclesView = new List<VehicleView>();


            foreach (var vehicle in vehicles)
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
                    CurrentBid = vehicle.CurrentBid
                };

                vehiclesView.Add(view);
            }
            return View(vehiclesView);
        }







        [Route("BindList")]
        public IActionResult BindList()
        {
            int id = int.Parse(HttpContext.Session.GetString("id"));

            var watch = this.dbContext.CurrentBinds.Where(x => x.UserId == id);

            List<Vehicle> vehicles = new List<Vehicle>();

            var vehiclesList = this.dbContext.Vehicles.ToList();

            foreach (var x in watch)
            {
                var dish = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId);
                vehicles.Add(dish);
            }


            List<VehicleView> vehiclesView = new List<VehicleView>();


            foreach (var vehicle in vehicles)
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
