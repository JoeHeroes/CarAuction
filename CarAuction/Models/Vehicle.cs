using CarAuction.Entites.Enum;

namespace CarAuction.Entites
{
    public class Vehicle
    {
        public int Id { get; set; }
        public Guid idVehicle { get; set; }
        public string producer { get; set; } = "";
        public string modelSpecifer { get; set; } = "";
        public string modelGeneration { get; set; } = "";
        public int registrationYear { get; set; }
        public string color { get; set; } = null!;
        public BodyType bodyType { get; set; }
        public int engineCapacity { get; set; }
        public int engineOutput { get; set; }
        public Transmission transmission { get; set; }
        public Drive drive { get; set; }
        public long meterReadout { get; set; }
        public Fuel fuel { get; set; }
        public string numberKeys { get; set; } = null!;
        public bool serviceManual { get; set; }
        public bool secondTireSet { get; set; }
        public int? CreateById { get; set; }
    }
}
