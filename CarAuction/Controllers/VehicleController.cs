using CarAuction.Models;
using CarAuction.Models.DTO;
using CarAuction.Models.Enum;
using CarAuction.Models.Selection;
using CarAuction.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Route("Finder")]
        public IActionResult Finder()
        {

            var producersTypes = SelectionListEnum.GetAllProducers();
            var years = SelectionListEnum.GetAllYears();
            var locations = SelectionListEnum.GetAllLocations();

            var model = new SearchVehicleDto();

            model.ProducerSelectList = new List<SelectListItem>();
            model.RegistrationYearSelectList = new List<SelectListItem>();
            model.ToYearSelectList = new List<SelectListItem>();
            model.SinceYearSelectList = new List<SelectListItem>();
            model.LocationSelectList = new List<SelectListItem>();


            foreach (var producer in producersTypes)
            {
                model.ProducerSelectList.Add(new SelectListItem { Text = producer.Name, Value = producer.Id });
            }

            foreach (var year in years)
            {
                model.RegistrationYearSelectList.Add(new SelectListItem { Text = year.Name, Value = year.Id });
            }
            foreach (var year in years)
            {
                model.ToYearSelectList.Add(new SelectListItem { Text = year.Name, Value = year.Id });
            }
            foreach (var year in years)
            {
                model.SinceYearSelectList.Add(new SelectListItem { Text = year.Name, Value = year.Id });
            }

            foreach (var location in locations)
            {
                model.LocationSelectList.Add(new SelectListItem { Text = location.Name, Value = location.Id });
            }

            return View(model);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            var bodyTypes = SelectionListEnum.GetAllBodyTypes();

            var damageTypes = SelectionListEnum.GetAllDamages();

            var driveTypes = SelectionListEnum.GetAllDrives();

            var fuelTypes = SelectionListEnum.GetAllFuels();

            var producersTypes = SelectionListEnum.GetAllProducers();

            var transmissionTypes = SelectionListEnum.GetAllTransmissions();

            var locationTypes = SelectionListEnum.GetAllLocations();

            var years = SelectionListEnum.GetAllYears();

            var model = new CreateVehicleDto();

            model.BodyTypeSelectList = new List<SelectListItem>();
            model.DamageSelectList = new List<SelectListItem>();
            model.DriveSelectList = new List<SelectListItem>();
            model.FuelSelectList = new List<SelectListItem>();
            model.RegistrationYearSelectList = new List<SelectListItem>();
            model.ProducerSelectList = new List<SelectListItem>();
            model.TransmissionSelectList = new List<SelectListItem>();
            model.LocationSelectList = new List<SelectListItem>();

            foreach (var body in bodyTypes)
            {
                model.BodyTypeSelectList.Add(new SelectListItem { Text = body.Name, Value = body.Id });
            }

            foreach (var damage in damageTypes)
            {
                model.DamageSelectList.Add(new SelectListItem { Text = damage.Name, Value = damage.Id });
            }

            foreach (var drive in driveTypes)
            {
                model.DriveSelectList.Add(new SelectListItem { Text = drive.Name, Value = drive.Id });
            }

            foreach (var fuel in fuelTypes)
            {
                model.FuelSelectList.Add(new SelectListItem { Text = fuel.Name, Value = fuel.Id });
            }

            foreach (var producer in producersTypes)
            {
                model.ProducerSelectList.Add(new SelectListItem { Text = producer.Name, Value = producer.Id });
            }

            foreach (var transmission in transmissionTypes)
            {
                model.TransmissionSelectList.Add(new SelectListItem { Text = transmission.Name, Value = transmission.Id });
            }
            foreach (var location in locationTypes)
            {
                model.LocationSelectList.Add(new SelectListItem { Text = location.Name, Value = location.Id });
            }
            foreach (var year in years)
            {
                model.RegistrationYearSelectList.Add(new SelectListItem { Text = year.Name, Value = year.Id });
            }
           


            return View(model);
        }

        public IActionResult WatchList()
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
                EngineCapacity = dto.EngineCapacity,
                EngineOutput = dto.EngineOutput,
                NumberKeys = dto.NumberKeys,
                ServiceManual = dto.ServiceManual,
                SecondTireSet = dto.SecondTireSet,
                Location = dto.Location,
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
        public IActionResult Search(SearchVehicleDto query)
        {
            var baseQuery = dbContext.Vehicles.Where(x => x.RegistrationYear >= query.SinceYear && x.RegistrationYear <= query.ToYear);

            List<string> searchPara = new List<string>();


            if (query.Producer != "none" && query.Producer != null)
            {
                var baseProducer = baseQuery.Where(x => x.Producer == query.Producer);
                baseQuery = baseProducer;
                searchPara.Add(query.Producer);
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


            if (query.LocationName != "none" && query.LocationName != null)
            {
                var baseLocation = baseQuery.Where(x => x.Location.Name == query.LocationName);
                baseQuery = baseLocation;
                searchPara.Add(query.LocationName);
            }

            if (query.RegistrationYear != 0)
            {
                var baseRegistration = baseQuery.Where(x => x.RegistrationYear == query.RegistrationYear);
                baseQuery = baseRegistration;
                searchPara.Add(query.RegistrationYear.ToString());
            }

            if (query.Damage != null)
            {
                var baseDamage = baseQuery.Where(x => x.PrimaryDamage == query.Damage);
                baseQuery = baseDamage;
                searchPara.Add(query.Damage);
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
        [Route("Lot")]
        public IActionResult Lot(int lotNumber)
        {
            Vehicle vehicle = this.dbContext
                                    .Vehicles
                                    .FirstOrDefault(x => x.Id == lotNumber);

            return View(vehicle);
        }

        [HttpPost]
        [Route("UpdateBid")]
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

                if (this.dbContext.CurrentBinds.FirstOrDefault(x => x.UserMany.Id == user.Id && x.VehicleMany.Id == vehicle.Id) == null)
                {
                    this.dbContext.CurrentBinds.Add(bind);
                }


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

            if(this.dbContext.Watches.FirstOrDefault(x => x.VehicleId == lotNumber) == null)
            {
                this.dbContext.Watches.Add(observed);
            }

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
                var veh = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId);

                if (veh != null)
                {
                    vehicles.Add(veh);
                }
            }

            if (vehicles == null)
            {
                return View(new List<VehicleView>());
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


        [Route("LotsWon")]
        public IActionResult LotsWon()
        {
            int id = int.Parse(HttpContext.Session.GetString("id"));

            var watch = this.dbContext.CurrentBinds.Where(x => x.UserId == id);

            List<Vehicle> vehicles = new List<Vehicle>();

            var vehiclesList = this.dbContext.Vehicles.ToList();

            foreach (var x in watch)
            {
                var veh = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId && d.WinnerId == id);

                if (veh != null)
                {
                    vehicles.Add(veh);
                }
            }

            if (vehicles == null)
            {
                return View(new List<VehicleView>());
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


        [Route("LotsLost")]
        public IActionResult LotsLost()
        {
            int id = int.Parse(HttpContext.Session.GetString("id"));

            var watch = this.dbContext.CurrentBinds.Where(x => x.UserId == id);

            List<Vehicle> vehicles = new List<Vehicle>();

            var vehiclesList = this.dbContext.Vehicles.ToList();

            foreach (var x in watch)
            {
                var veh = vehiclesList.FirstOrDefault(d => d.Id == x.VehicleId && d.WinnerId != id);

                if(veh != null)
                {
                    vehicles.Add(veh);
                }
            }

           if(vehicles == null)
           {
               return View(new List<VehicleView>());
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