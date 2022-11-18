using CarAuction.Models.Enum;

namespace CarAuction.Models.View
{
    public class VehicleView
    {
        public int PathPic { get; set; }
        public Guid LotNumber { get; set; }
        public bool Watch { get; set; }
        public int RegistrationYear { get; set; }
        public Producer Producer { get; set; }
        public string ModelSpecifer { get; set; }
        public DateTime DateTime { get; set; }
        public long MeterReadout { get; set; }
        public Damage Damage { get; set; }
        public int CurrentBid { get; set; }

    }
}
