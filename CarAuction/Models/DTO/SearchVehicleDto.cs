using CarAuction.Models.Enum;

namespace CarAuction.Models.DTO
{
    public class SearchVehicleDto
    {
        public Producer Producer { get; set; }
        public int SienceYear { get; set; }
        public int ToeYear { get; set; }
        public Location Location { get; set; }
    }
}
