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

        [Route("AddVehicle")]
        public IActionResult AddVehicle()
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

            var picture = new Picture { PathImg = stringFileName };
            dbContext.Pictures.Add(picture);

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
                    LocationId = dto.LocationId,
                    Fuel = dto.Fuel,
                    PrimaryDamage = dto.PrimaryDamage,
                    SecondaryDamage = dto.SecondaryDamage,
                    VIN = dto.VIN,
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
                var locationResult = this.dbContext.Locations.FirstOrDefault(x => x.Name == query.LocationName);
                var baseLocation = baseQuery.Where(x => x.LocationId == locationResult.Id);
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
                    { nameof(Vehicle.LocationId), r => r.LocationId },
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

                

                List<string> pictures = new List<string>();

                var restultPictures = this.dbContext.Pictures.Where(x => x.Id == vehicle.Id);
                if (restultPictures != null)
                {
                     foreach (var pic in restultPictures)
                        {
                            pictures.Add(pic.PathImg);
                        }
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
                    CurrentBid = vehicle.CurrentBid
                };

                vehiclesView.Add(view);
            }

            SearchModelDto model = new();

            model.Vehicles = vehiclesView;
            model.Options = query;

            return View(model);
        }
        [Route("Lot")]
        public IActionResult Lot(int lotNumber)
        {
            Vehicle vehicle = this.dbContext
                                    .Vehicles
                                    .FirstOrDefault(x => x.Id == lotNumber);

            var watchResult = this.dbContext.Watches.Where(x => x.VehicleId == vehicle.Id);
            bool watchBool = false;

            if (HttpContext.Session.GetString("id") != null)
            {
                var result = watchResult.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("id")));

                if (result != null)
                {
                    watchBool = true;
                }

            }
           

            var location = this.dbContext.Locations.FirstOrDefault(x => x.Id == vehicle.LocationId);

            var restultPictures = this.dbContext.Pictures.Where(x => x.VehicleId == vehicle.Id);

            List<string> pictures = new List<string>();

            foreach (var pic in restultPictures)
            {
                pictures.Add(pic.PathImg);
            }

            VehicleLotView view = new VehicleLotView()
            {
                Id = vehicle.Id,
                Producer = vehicle.Producer,
                ModelSpecifer = vehicle.Producer,
                ModelGeneration = vehicle.ModelGeneration,
                RegistrationYear = vehicle.RegistrationYear,
                Color = vehicle.Color,
                BodyType = vehicle.BodyType,
                EngineCapacity = vehicle.EngineCapacity,
                EngineOutput = vehicle.EngineOutput,
                Transmission = vehicle.Transmission,
                Drive = vehicle.Drive,
                MeterReadout = vehicle.MeterReadout,
                Fuel = vehicle.Fuel,
                NumberKeys = vehicle.NumberKeys,
                ServiceManual = vehicle.ServiceManual,
                SecondTireSet = vehicle.SecondTireSet,
                CreateById = vehicle.CreateById,
                Images = pictures,
                Location = location.Name,
                PrimaryDamage = vehicle.PrimaryDamage,
                VIN = vehicle.VIN,
                Highlights = vehicle.Highlights,
                DateTime = vehicle.DateTime,
                CurrentBid = vehicle.CurrentBid,
                Watch = watchBool,
                WinnerId = vehicle.WinnerId,


            };
            return View(view);

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

                var bind = new Bind()
                {
                    UserId = user.Id,
                    VehicleId = vehicle.Id,
                };

                if (this.dbContext.Binds.FirstOrDefault(x => x.UserId == user.Id && x.VehicleId == vehicle.Id) == null)
                {
                    this.dbContext.Binds.Add(bind);
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
                UserId = user.Id,
                VehicleId = vehicle.Id,
            };

            if(this.dbContext.Watches.FirstOrDefault(x => x.VehicleId == lotNumber) == null)
            {
                var newEvent = new Event()
                {
                    Title = vehicle.Id +" "+ vehicle.Producer + " " + vehicle.ModelGeneration ,
                    Start = vehicle.DateTime.ToString("yyyy-MM-dd"),
                    End = vehicle.DateTime.ToString("yyyy-MM-dd"),
                    Color = vehicle.Color,
                    AllDay = true,
                    Owner = user.Id,
                };


                this.dbContext.Events.Add(newEvent);
                this.dbContext.Watches.Add(observed);
            }

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


            var events = this.dbContext.Events.FirstOrDefault(x => x.Title == vehicle.Id + " " + vehicle.Producer + " " + vehicle.ModelGeneration);
            
            
            
            if (events != null && events.Owner == id)
            {
                this.dbContext.Events.Remove(events);
            }

            this.dbContext.Watches.Remove(observed);

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


                var watchResult = this.dbContext.Watches.Where(x => x.VehicleId == vehicle.Id);
                var result = watchResult.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("id")));

                bool watchBool = false;

                if (result != null)
                {
                    watchBool = true;
                }

                var restultPictures = this.dbContext.Pictures.Where(x => x.Id == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
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

            var watch = this.dbContext.Binds.Where(x => x.UserId == id);

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
                var watchResult = this.dbContext.Watches.Where(x => x.VehicleId == vehicle.Id);
                var result = watchResult.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("id")));

                bool watchBool = false;

                if (result != null)
                {
                    watchBool = true;
                }

                var restultPictures = this.dbContext.Pictures.Where(x => x.Id == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
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


        [Route("LotsWon")]
        public IActionResult LotsWon()
        {
            int id = int.Parse(HttpContext.Session.GetString("id"));

            var watch = this.dbContext.Binds.Where(x => x.UserId == id);

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
                var watchResult = this.dbContext.Watches.Where(x => x.VehicleId == vehicle.Id);
                var result = watchResult.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("id")));

                bool watchBool = false;

                if (result != null)
                {
                    watchBool = true;
                }

                var restultPictures = this.dbContext.Pictures.Where(x => x.Id == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
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


        [Route("LotsLost")]
        public IActionResult LotsLost()
        {
            int id = int.Parse(HttpContext.Session.GetString("id"));

            var watch = this.dbContext.Binds.Where(x => x.UserId == id);

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
                var watchResult = this.dbContext.Watches.Where(x => x.VehicleId == vehicle.Id);
                var result = watchResult.FirstOrDefault(x => x.UserId == int.Parse(HttpContext.Session.GetString("id")));

                bool watchBool = false;

                if (result != null)
                {
                    watchBool = true;
                }

                var restultPictures = this.dbContext.Pictures.Where(x => x.Id == vehicle.Id);

                List<string> pictures = new List<string>();

                foreach (var pic in restultPictures)
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

        public IActionResult ListVehicle()
        {
            var vehicles = this.dbContext.Vehicles.ToList();


            List<VehicleView> vehiclesView = new List<VehicleView>();


            foreach (var vehicle in vehicles)
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

                foreach (var pic in restultPictures)
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
                    CurrentBid = vehicle.CurrentBid
                };

                vehiclesView.Add(view);
            }

            return View(vehiclesView);
        }


        [Route("EditVehicle")]
        public IActionResult EditVehicle(int id)
        {
            var vehicle = this.dbContext.Vehicles.FirstOrDefault(x => x.Id == id);

            var model = new EditVehicleDto()
            {
                Id = vehicle.Id,
                RegistrationYear = vehicle.RegistrationYear,
                Color = vehicle.Color,
                BodyType = vehicle.BodyType,
                Transmission = vehicle.Transmission,
                LocationId = vehicle.LocationId,
                Fuel = vehicle.Fuel,
                DateTime = vehicle.DateTime,
            };

            var bodyTypes = SelectionListEnum.GetAllBodyTypes();

            var fuelTypes = SelectionListEnum.GetAllFuels();

            var transmissionTypes = SelectionListEnum.GetAllTransmissions();

            var locationTypes = SelectionListEnum.GetAllLocations();

            var years = SelectionListEnum.GetAllYears();

            model.BodyTypeSelectList = new List<SelectListItem>();
            model.FuelSelectList = new List<SelectListItem>();
            model.RegistrationYearSelectList = new List<SelectListItem>();
            model.TransmissionSelectList = new List<SelectListItem>();
            model.LocationSelectList = new List<SelectListItem>();

            foreach (var body in bodyTypes)
            {
                model.BodyTypeSelectList.Add(new SelectListItem { Text = body.Name, Value = body.Id });
            }

            foreach (var fuel in fuelTypes)
            {
                model.FuelSelectList.Add(new SelectListItem { Text = fuel.Name, Value = fuel.Id });
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

        [HttpPost]
        [Route("EditVehicle")]
        public IActionResult EditVehicle(EditVehicleDto dto)
        {
            if (ModelState.IsValid)
            {
                var vehicle = this.dbContext.Vehicles.FirstOrDefault(x => x.Id == dto.Id);

                vehicle.RegistrationYear = dto.RegistrationYear;
                vehicle.Color = dto.Color;
                vehicle.BodyType = dto.BodyType;
                vehicle.Transmission = dto.Transmission;
                vehicle.LocationId = dto.LocationId;
                vehicle.Fuel = dto.Fuel;
                vehicle.DateTime = dto.DateTime;

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
                return RedirectToAction("ListVehicle");
            }
            return RedirectToAction("EditVehicle");
        }

      
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = this.dbContext.Vehicles.FirstOrDefault(x => x.Id == id);
            if (vehicle != null)
            {
                this.dbContext.Vehicles.Remove(vehicle);
            }

            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

            return RedirectToAction("ListVehicle");
        }
    }
}