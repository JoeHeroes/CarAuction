using CarAuction.Models.Enum;

namespace CarAuction.Models.DTO
{
    public class UpdateVehicleDto
    {
        public long MeterReadout { get; set; }
        public bool ServiceManual { get; set; }
        public bool SecondTireSet { get; set; }
        public SaleTerm SaleTerm { get; set; }
        public Damage PrimaryDamage { get; set; }
        public Damage SecondaryDamage { get; set; }
    }
}
