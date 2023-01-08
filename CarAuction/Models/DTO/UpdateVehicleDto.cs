using CarAuction.Models.Enum;

namespace CarAuction.Models.DTO
{
    public class UpdateVehicleDto
    {
        public long MeterReadout { get; set; }
        public bool ServiceManual { get; set; }
        public bool SecondTireSet { get; set; }
        public SaleTerm SaleTerm { get; set; }
        public string PrimaryDamage { get; set; }
        public string SecondaryDamage { get; set; }
    }
}
