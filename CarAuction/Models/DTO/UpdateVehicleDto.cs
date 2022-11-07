using CarAuction.Entites.Enum;

namespace CarAuction.Entities.Action
{
    public class UpdateVehicleDto
    {
        public long meterReadout { get; set; }
        public bool serviceManual { get; set; }
        public bool secondTireSet { get; set; }
        public SaleTerm saleTerm { get; set; }
        public Damage primaryDamage { get; set; }
        public Damage secondaryDamage { get; set; }
    }
}
