using CarAuction.Authorization;
using CarAuction.Entites;
using CarAuction.Entites.Enum;

namespace CarAuction
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
                    color = "Blue",
                    bodyType = BodyType.Sedan,
                    engineCapacity = 3900,
                    engineOutput = 225,
                    transmission = Transmission.Manual,
                    drive = Drive.AWD,
                    meterReadout = 176000,
                    fuel = Fuel.Diesel,
                    numberKeys = "2",
                    serviceManual = true,
                    secondTireSet = true,
                },
                new Vehicle()
                {
                    color = "Red",
                    bodyType = BodyType.Sedan,
                    engineCapacity = 1998,
                    engineOutput = 110,
                    transmission = Transmission.Automatic,
                    drive = Drive.AWD,
                    meterReadout = 65000,
                    fuel = Fuel.Diesel,
                    numberKeys = "2",
                    serviceManual = true,
                    secondTireSet = true,
                }
            };
        }
    }
}
