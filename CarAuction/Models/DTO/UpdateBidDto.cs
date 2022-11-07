using CarAuction.Entites.Enum;

namespace CarAuction.Entities.Action
{
    public class UpdateBidDto
    {
        public Location location { get; set; }
        public DateTime dateTime { get; set; }
        public DateTime timeLeft { get; set; }
    }
}
