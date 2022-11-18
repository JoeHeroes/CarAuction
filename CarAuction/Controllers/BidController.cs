using CarAuction.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoAuction.Controllers
{
    public class BidController : Controller
    {
        private readonly AuctionDbContext dbContext;

        public BidController(AuctionDbContext dbContext)
        {
           this.dbContext = dbContext;
        }
    }
}
