using System;
using CarAuction.Models.Enum;

namespace CarAuction.Models
{
    public class Sell
    {
        public int Id { get; set; }
        public SaleTerm SaleTerm { get; set; }
        public Damage PrimaryDamage { get; set; }
        public Damage SecondaryDamage { get; set; }
        public string VIN { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public DateTime TimeLeft { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
