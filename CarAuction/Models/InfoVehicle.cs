using CarAuction.Models.Enum;
using System.Net;

namespace CarAuction.Models
{
    public class InfoVehicle
    {
        public int Id { get; set; }
        public Guid LotNumber { get; set; }
        public Producer Producer { get; set; }
        public string ModelSpecifer { get; set; }
        public string ModelGeneration { get; set; } 
        public int RegistrationYear { get; set; }
        public string Color { get; set; }
        public BodyCar BodyType { get; set; }
        public int EngineCapacity { get; set; }
        public int EngineOutput { get; set; }
        public Transmission Transmission { get; set; }
        public Drive Drive { get; set; }
        public long MeterReadout { get; set; }
        public Fuel Fuel { get; set; }
        public string NumberKeys { get; set; }
        public bool ServiceManual { get; set; }
        public bool SecondTireSet { get; set; }
        public int CreateById { get; set; }
        public InfoSell InfoSell { get; set; }
        public InfoBid InfoBid { get; set; }
        public Location Location { get; set; }

    }
}
