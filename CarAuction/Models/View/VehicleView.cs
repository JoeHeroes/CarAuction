using CarAuction.Models.Enum;

namespace CarAuction.Models.View
{
    public class VehicleView
    {
        public int LotNumber { get; set; }
        public int RegistrationYear { get; set; }
        public string Producer { get; set; }
        public string ModelSpecifer { get; set; }
        public DateTime DateTime { get; set; }
        public long MeterReadout { get; set; }
        public string Damage { get; set; }
        public int CurrentBid { get; set; }
        public List<string> Images { get; set; }
        public string SearchBy { get; set; }
        public bool Watch { get; set; }
        public int WinnerId { get; set; }
    }
}
