using CarAuction.Authorization;
using CarAuction.Models;
using CarAuction.Models.Enum;

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
                    Producer = Producer.Audi,
                    ModelSpecifer = "A4",
                    ModelGeneration = "B9",
                    RegistrationYear = 2016,
                    Color = "Grey",
                    BodyType = BodyCar.Sedan,
                    EngineCapacity = 1968 ,
                    EngineOutput = 110,
                    Transmission = Transmission.DualClutch,
                    Drive = Drive.FWD,
                    MeterReadout = 177000,
                    Fuel = Fuel.Diesel,
                    NumberKeys = "1",
                    ServiceManual = true,
                    SecondTireSet = true,
                    Bid = new Bid()
                    {
                        Watch = true,
                        CurrentBid = 5000
                    },
                    Sell = new Sell()
                    {
                        PrimaryDamage = Damage.Normal_Wear,
                        DateTime = DateTime.Today,
                        VIN = ""
                    }
                },
                new Vehicle()
                {
                    Producer = Producer.Audi,
                    ModelSpecifer = "A4",
                    ModelGeneration = "B6",
                    RegistrationYear = 2004,
                    Color = "Blue",
                    BodyType = BodyCar.Sedan,
                    EngineCapacity = 1896,
                    EngineOutput = 96,
                    Transmission = Transmission.Manual,
                    Drive = Drive.FWD,
                    MeterReadout = 268000,
                    Fuel = Fuel.Diesel,
                    NumberKeys = "1",
                    ServiceManual = true,
                    SecondTireSet = false,
                    Bid = new Bid()
                    {
                        Watch = true,
                        CurrentBid = 5000
                    },
                    Sell = new Sell()
                    {
                        PrimaryDamage = Damage.Normal_Wear,
                        DateTime = DateTime.Today,
                        VIN = ""
                    }
                },
                new Vehicle()
                {
                    Producer = Producer.Audi,
                    ModelSpecifer = "Passat",
                    ModelGeneration = "B8",
                    RegistrationYear = 2015,
                    Color = "Black",
                    BodyType = BodyCar.Sedan,
                    EngineCapacity = 1598,
                    EngineOutput = 88,
                    Transmission = Transmission.DualClutch,
                    Drive = Drive.FWD,
                    MeterReadout = 180000,
                    Fuel = Fuel.Diesel,
                    NumberKeys = "2",
                    ServiceManual = true,
                    SecondTireSet = true,
                    Bid = new Bid()
                    {
                        Watch = true,
                        CurrentBid = 5000 
                    },
                    Sell = new Sell()
                    {
                        PrimaryDamage = Damage.Normal_Wear,
                        DateTime = DateTime.Today,
                        VIN = ""
                    }
                },
                new Vehicle()
                {
                    Producer = Producer.Volkswagen,
                    ModelSpecifer = "Passat",
                    ModelGeneration = "B8",
                    RegistrationYear = 2015,
                    Color = "LightBlue",
                    BodyType = BodyCar.Sedan,
                    EngineCapacity = 1968,
                    EngineOutput = 110,
                    Transmission = Transmission.DualClutch,
                    Drive = Drive.FWD,
                    MeterReadout = 172000,
                    Fuel = Fuel.Diesel,
                    NumberKeys = "2",
                    ServiceManual = true,
                    SecondTireSet = true,
                    Bid = new Bid()
                    {
                        Watch = true,
                        CurrentBid = 5000
                    },
                    Sell = new Sell()
                    {
                        PrimaryDamage = Damage.Normal_Wear,
                        DateTime = DateTime.Today,
                        VIN = ""
                    }
                },
                new Vehicle()
                {
                    Producer = Producer.Volkswagen,
                    ModelSpecifer = "Polo",
                    ModelGeneration = "2G",
                    RegistrationYear = 2020,
                    Color = "White",
                    BodyType = BodyCar.Hatchback,
                    EngineCapacity = 999,
                    EngineOutput = 59,
                    Transmission = Transmission.Manual,
                    Drive = Drive.FWD,
                    MeterReadout = 25000,
                    Fuel = Fuel.Petrol,
                    NumberKeys = "2",
                    ServiceManual = true,
                    SecondTireSet = true,
                    Bid = new Bid()
                    {
                        Watch = true,
                        CurrentBid = 5000
                    },
                    Sell = new Sell()
                    {
                        PrimaryDamage = Damage.Normal_Wear,
                        DateTime = DateTime.Today,
                        VIN = ""
                    }
                },
                new Vehicle()
                {
                    Producer = Producer.Toyota,
                    ModelSpecifer = "Yaris",
                    ModelGeneration = "2",
                    RegistrationYear = 2010,
                    Color = "White",
                    BodyType = BodyCar.Hatchback,
                    EngineCapacity = 1364,
                    EngineOutput = 66,
                    Transmission = Transmission.Manual,
                    Drive = Drive.FWD,
                    MeterReadout = 80000,
                    Fuel = Fuel.Diesel,
                    NumberKeys = "2",
                    ServiceManual = false,
                    SecondTireSet = false,
                    Bid = new Bid()
                    {
                        Watch = true,
                        CurrentBid = 5000
                    },
                    Sell = new Sell()
                    {
                        PrimaryDamage = Damage.Normal_Wear,
                        DateTime = DateTime.Today,
                        VIN = ""
                    }
                }
            };
        }
    }
}
