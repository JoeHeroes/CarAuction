﻿using CarAuction.Models.Enum;
using System.ComponentModel;
using System.Net;

namespace CarAuction.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Producer { get; set; }
        [DisplayName("Model Specifer")]
        public string ModelSpecifer { get; set; }
        [DisplayName("Model Generation")]
        public string ModelGeneration { get; set; }
        [DisplayName("Registration Year")]
        public int RegistrationYear { get; set; }
        public string Color { get; set; }
        [DisplayName("Body")]
        public string BodyType { get; set; }
        [DisplayName("Engine Capacity")]
        public int EngineCapacity { get; set; }
        [DisplayName("Engine Output")]
        public int EngineOutput { get; set; }
        public string Transmission { get; set; }
        public string Drive { get; set; }
        [DisplayName("Meter Readout")]
        public long MeterReadout { get; set; }
        public string Fuel { get; set; }
        [DisplayName("Number Keys")]
        public string NumberKeys { get; set; }
        [DisplayName("Service Manual")]
        public bool ServiceManual { get; set; }
        [DisplayName("Second Tire Set")]
        public bool SecondTireSet { get; set; }
        [DisplayName("Owner")]
        public int CreateById { get; set; }
        public string ProfileImg { get; set; }
        public Location Location { get; set; }


        //Sell
        [DisplayName("First Name")]

        public string PrimaryDamage { get; set; }
        [DisplayName("First Name")]

        public string SecondaryDamage { get; set; }
        public string VIN { get; set; } = null!;
        public Highlight Highlights { get; set; }
        [DisplayName("First Name")]
        public DateTime DateTime { get; set; }
      

        //Bid
        public bool BidStatus { get; set; }
        public int CurrentBid { get; set; }
        public int WinnerId { get; set; }
        public bool Watch { get; set; }
        public virtual List<Watch> Bidders { get; set; }
        public virtual List<CurrentBind> CurrentBinds { get; set; }
    }
}
