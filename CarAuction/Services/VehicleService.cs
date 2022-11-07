using AutoAuction.Exceptions;
using CarAuction.Entites;
using CarAuction.Entities.Action;
using CarAuction.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AutoAuction.Service
{
    public interface IVehicleService
    {
        int Create(CreateVehicleDto dto);
        void Delete(int id);
        IEnumerable<Vehicle> GetAll();
        Vehicle GetById(int id);
        void Update(int id, UpdateVehicleDto dto);
        void Inport();
        void Export();
    }
    public class VehicleService : IVehicleService
    {

        private readonly AuctionDbContext dbContext;
        private readonly ILogger<VehicleService> logger;

        public VehicleService(AuctionDbContext dbContext, ILogger<VehicleService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public IEnumerable<Vehicle> GetAll()
        {
            var vehicles = this.dbContext
              .Vehicles
              .ToList();

            if (vehicles is null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            return vehicles;
        }

        public Vehicle GetById(int id)
        {

            var vehicles = this.dbContext
                .Vehicles
                .FirstOrDefault(u => u.Id == id);

            if (vehicles is null)
            {
                throw new NotFoundException("Student not found");
            }

            return vehicles;
        }


        public int Create(CreateVehicleDto dto)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            logger.LogWarning($"Vehicle with id: {id} DELETE action invoked");

            var vehicles = this.dbContext
               .Vehicles
               .FirstOrDefault(u => u.Id == id);

            if (vehicles is null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            dbContext.Vehicles.Remove(vehicles);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public void Update(int id, UpdateVehicleDto model)
        {
            var vehicles = this.dbContext
                 .Vehicles
                 .FirstOrDefault(u => u.Id == id);

            if (vehicles is null)
            {
                throw new NotFoundException("Student not found");
            }



            vehicles.meterReadout = model.meterReadout;
            vehicles.serviceManual = model.serviceManual;
            vehicles.secondTireSet = model.secondTireSet;


            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }

        }

        public void Inport()
        {
            throw new NotImplementedException();
        }

        public void Export()
        {
            throw new NotImplementedException();
        }
    }

}
