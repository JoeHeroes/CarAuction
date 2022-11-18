using CarAuction.Models.Enum;

namespace CarAuction.Models.DTO
{
    public class UpdateBidDto
    {
        public Location Location { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime TimeLeft { get; set; }
    }
}
