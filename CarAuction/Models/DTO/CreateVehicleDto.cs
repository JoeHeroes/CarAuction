using CarAuction.Models.Enum;

namespace CarAuction.Models.DTO
{
    public class CreateVehicleDto
    {
        public string color { get; set; } = null!;
        public BodyCar bodyType { get; set; }
        public int engineCapacity { get; set; }
        public int engineOutput { get; set; }
        public Transmission transmission { get; set; }
        public Drive drive { get; set; }
        public long meterReadout { get; set; }
        public Fuel fuel { get; set; }
        public int numberKeys { get; set; }
        public bool serviceManual { get; set; }
        public bool secondTireSet { get; set; }




        public string modelSpecifer { get; set; } = null!;
        public string modelGeneration { get; set; } = null!;
        public int registrationYear { get; set; }
        public SaleTerm saleTerm { get; set; }
        public Damage primaryDamage { get; set; }
        public Damage secondaryDamage { get; set; }
        public string VIN { get; set; } = null!;
        public string highLights { get; set; } = null!;
    }
}
