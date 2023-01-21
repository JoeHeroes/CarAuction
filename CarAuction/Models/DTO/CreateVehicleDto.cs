using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace CarAuction.Models.DTO
{
    public class CreateVehicleDto
    {
        public int Id { get; set; }

        public string Producer { get; set; }
        public List<SelectListItem> ProducerSelectList { get; set; }

        [DisplayName("Model Specifer")]
        public string ModelSpecifer { get; set; }
        [DisplayName("Model Generation")]
        public string ModelGeneration { get; set; }
        [DisplayName("Registration Year")]
        public int RegistrationYear { get; set; }
        public List<SelectListItem> RegistrationYearSelectList { get; set; }

        public string Color { get; set; }

        [DisplayName("Body Type")]
        public string BodyType { get; set; }
        public List<SelectListItem> BodyTypeSelectList { get; set; }

        public string Transmission { get; set; }
        public List<SelectListItem> TransmissionSelectList { get; set; }

        public string Drive { get; set; }
        public List<SelectListItem> DriveSelectList { get; set; }

        [DisplayName("Meter Readout")]
        public long MeterReadout { get; set; }


        public string Fuel { get; set; }
        public List<SelectListItem> FuelSelectList { get; set; }

        [DisplayName("Primary Damage")]
        public string PrimaryDamage { get; set; }
        [DisplayName("Secondary Damage")]
        public string SecondaryDamage { get; set; }
        public List<SelectListItem> DamageSelectList { get; set; }




        [DisplayName("Engine Capacity")]
        public int EngineCapacity { get; set; }
        [DisplayName("Engine Output")]
        public int EngineOutput { get; set; }
        [DisplayName("Number Keys")]
        public string NumberKeys { get; set; }
        [DisplayName("Service Manual")]
        public bool ServiceManual { get; set; }
        [DisplayName("Second Tire Set")]
        public bool SecondTireSet { get; set; }

        public Location Location { get; set; }
        public List<SelectListItem> LocationSelectList { get; set; }



        public string VIN { get; set; } = null!;
        public IFormFile PathPic { get; set; }
        [DisplayName("Date of Auction")]
        public DateTime DateTime { get; set; }

    }
}
