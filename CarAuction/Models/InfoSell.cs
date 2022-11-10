using System;
using CarAuction.Entites.Enum;

namespace CarAuction.Models
{
    public class InfoSell
    {
        public int Id { get; set; }
        public Guid idVehicle { get; set; }
        public SaleTerm saleTerm { get; set; }
        public Damage primaryDamage { get; set; }
        public Damage secondaryDamage { get; set; }
        public string VIN { get; set; } = null!;
        public string highLights { get; set; } = null!;
    }
}
