using CarAuction.Models.View;

namespace CarAuction.Models.DTO
{
    public class SearchModelDto
    {
        public List<VehicleView> Vehicles { get; set; }
        public SearchVehicleDto Options { get; set; }

        public SearchModelDto()
        {
            this.Vehicles = new List<VehicleView>();
            this.Options = new SearchVehicleDto();
        }
    }
}
