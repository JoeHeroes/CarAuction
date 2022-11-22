using CarAuction.Models.Enum;

namespace CarAuction.Models.DTO
{
    public class CreateVehicleDto
    {
        public int Id { get; set; }
        public Producer Producer { get; set; }
        public string ModelSpecifer { get; set; }
        public string ModelGeneration { get; set; }
        public int RegistrationYear { get; set; }
        public string Color { get; set; }
        public BodyCar BodyType { get; set; }
        public Transmission Transmission { get; set; }
        public Drive Drive { get; set; }
        public long MeterReadout { get; set; }
        public Fuel Fuel { get; set; }
        public Damage PrimaryDamage { get; set; }
        public Damage SecondaryDamage { get; set; }
        public string VIN { get; set; } = null!;
        public IFormFile PathPic { get; set; }


    }
}
