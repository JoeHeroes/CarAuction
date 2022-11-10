using CarAuction.Authorization;
using CarAuction.Entites;
using CarAuction.Entites.Enum;

namespace CarAuction.Seeder
{
    public class AuctionSeeder
    {
        private readonly AuctionDbContext _dbContext;

        public AuctionSeeder(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {

                if (!_dbContext.Roles.Any())
                {
                    var info = GetRole();
                    _dbContext.Roles.AddRange(info);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Vehicles.Any())
                {
                    var info = GetInfoVehicle();
                    _dbContext.Vehicles.AddRange(info);
                    _dbContext.SaveChanges();
                }
            }

        }

        public IEnumerable<Role> GetRole()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };
        }

        public IEnumerable<Vehicle> GetInfoVehicle()
        {
            return new List<Vehicle>()
            {
                new Vehicle()
                {
                    idVehicle = new Guid(),
                    producer = "Audi",
                    modelSpecifer = "A4",
                    modelGeneration = "B9",
                    registrationYear = 2016,
                    color = "Grey",
                    bodyType = BodyType.Sedan,
                    engineCapacity = 1968 ,
                    engineOutput = 110,
                    transmission = Transmission.DualClutch,
                    drive = Drive.FWD,
                    meterReadout = 177000,
                    fuel = Fuel.Diesel,
                    numberKeys = "1",
                    serviceManual = true,
                    secondTireSet = true,
                },
                new Vehicle()
                {
                    idVehicle = new Guid(),
                    producer = "Audi",
                    modelSpecifer = "A4",
                    modelGeneration = "B6",
                    registrationYear = 2004,
                    color = "Blue",
                    bodyType = BodyType.Sedan,
                    engineCapacity = 1896,
                    engineOutput = 96,
                    transmission = Transmission.Manual,
                    drive = Drive.FWD,
                    meterReadout = 268000,
                    fuel = Fuel.Diesel,
                    numberKeys = "1",
                    serviceManual = true,
                    secondTireSet = false,
                },
                new Vehicle()
                {
                    idVehicle = new Guid(),
                    producer = "VW",
                    modelSpecifer = "Passat",
                    modelGeneration = "B8",
                    registrationYear = 2015,
                    color = "Black",
                    bodyType = BodyType.Sedan,
                    engineCapacity = 1598,
                    engineOutput = 88,
                    transmission = Transmission.DualClutch,
                    drive = Drive.FWD,
                    meterReadout = 180000,
                    fuel = Fuel.Diesel,
                    numberKeys = "2",
                    serviceManual = true,
                    secondTireSet = true,
                },
                new Vehicle()
                {
                    idVehicle = new Guid(),
                    producer = "VW",
                    modelSpecifer = "Passat",
                    modelGeneration = "B8",
                    registrationYear = 2015,
                    color = "LightBlue",
                    bodyType = BodyType.Sedan,
                    engineCapacity = 1968,
                    engineOutput = 110,
                    transmission = Transmission.DualClutch,
                    drive = Drive.FWD,
                    meterReadout = 172000,
                    fuel = Fuel.Diesel,
                    numberKeys = "2",
                    serviceManual = true,
                    secondTireSet = true,
                },
                new Vehicle()
                {
                    idVehicle = new Guid(),
                    producer = "VW",
                    modelSpecifer = "Polo",
                    modelGeneration = "2G",
                    registrationYear = 2020,
                    color = "White",
                    bodyType = BodyType.Hatchback,
                    engineCapacity = 999,
                    engineOutput = 59,
                    transmission = Transmission.Manual,
                    drive = Drive.FWD,
                    meterReadout = 25000,
                    fuel = Fuel.Petrol,
                    numberKeys = "2",
                    serviceManual = true,
                    secondTireSet = true,
                },
                new Vehicle()
                {
                    idVehicle = new Guid(),
                    producer = "Toyota",
                    modelSpecifer = "Yaris",
                    modelGeneration = "2",
                    registrationYear = 2010,
                    color = "White",
                    bodyType = BodyType.Hatchback,
                    engineCapacity = 1364,
                    engineOutput = 66,
                    transmission = Transmission.Manual,
                    drive = Drive.FWD,
                    meterReadout = 80000,
                    fuel = Fuel.Diesel,
                    numberKeys = "2",
                    serviceManual = false,
                    secondTireSet = false,
                }
            };
        }
    }
}
