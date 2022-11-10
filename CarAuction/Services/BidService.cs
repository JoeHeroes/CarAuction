using AutoAuction.Exceptions;
using CarAuction.Entites;
using CarAuction.Entites.Enum;
using CarAuction.Entities.Action;
using CarAuction.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Services
{
    public interface IBidService
    {
        int Create(CreateBidDto dto);
        void Delete(int id);
        IEnumerable<BidStatus> GetAll();
        BidStatus GetById(int id);
        void Update(int id, UpdateBidDto model);
    }
    public class BidService: IBidService
    {

        private readonly AuctionDbContext dbContext;
        private readonly ILogger<BidService> logger;

        public BidService(AuctionDbContext dbContext, ILogger<BidService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }


        public IEnumerable<BidStatus> GetAll()
        {
            var bid = this.dbContext.Bids.ToList();

            if (bid is null)
            {
                throw new NotFoundException("Student not found");
            }

            return bid;
        }

        public BidStatus GetById(int id)
        {
            var bid = this.dbContext.Bids.FirstOrDefault(u => u.Id == id);

            if (bid is null)
            {
                throw new NotFoundException("Student not found");
            }

            return bid;
        }

        public int Create(CreateBidDto dto)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            logger.LogWarning($"Bid with id: {id} DELETE action invoked");

            var bid = this.dbContext
               .Bids
               .FirstOrDefault(u => u.Id == id);

            if (bid is null)
            {
                throw new NotFoundException("Bid not found");
            }

            dbContext.Bids.Remove(bid);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }

        public void Update(int id, UpdateBidDto model)
        {
            var bid = this.dbContext
                 .Bids
                 .FirstOrDefault(u => u.Id == id);

            if (bid is null)
            {
                throw new NotFoundException("Bid not found");
            }



            bid.location = model.location;
            bid.dateTime = model.dateTime;
            bid.timeLeft = model.timeLeft;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error DataBase", e);
            }
        }
    }
}
