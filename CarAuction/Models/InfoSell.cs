using System;
using CarAuction.Entites.Enum;

namespace CarAuction.Models
{
    public class InfoSell
    {
        public int Id { get; set; }
        public int idVehicle { get; set; }
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
