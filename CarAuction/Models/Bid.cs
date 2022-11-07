using System;
using CarAuction.Entites.Enum;

namespace CarAuction.Entites
{
    public class Bid
    {
        public int Id { get; set; }
        public int idVehicle { get; set; }
        public bool bidStatus { get; set;}
        public int currentBid { get; set; }
        public bool saleStatus { get; set; }
        public Location location { get; set; }
        public DateTime dateTime { get; set; }
        public DateTime timeLeft { get; set; }
    }
}
