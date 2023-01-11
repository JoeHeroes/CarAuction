using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarAuction.Models.DTO
{
    public class CreateVehicleDto
    {
        public int Id { get; set; }

        public string Producer { get; set; }
        public List<SelectListItem> ProducerSelectList { get; set; }


        public string ModelSpecifer { get; set; }
        public string ModelGeneration { get; set; }

        public int RegistrationYear { get; set; }
        public List<SelectListItem> RegistrationYearSelectList { get; set; }

        public string Color { get; set; }


        public string BodyType { get; set; }
        public List<SelectListItem> BodyTypeSelectList { get; set; }

        public string Transmission { get; set; }
        public List<SelectListItem> TransmissionSelectList { get; set; }

        public string Drive { get; set; }
        public List<SelectListItem> DriveSelectList { get; set; }


        public long MeterReadout { get; set; }


        public string Fuel { get; set; }
        public List<SelectListItem> FuelSelectList { get; set; }


        public string PrimaryDamage { get; set; }
        public string SecondaryDamage { get; set; }
        public List<SelectListItem> DamageSelectList { get; set; }



        public string VIN { get; set; } = null!;
        public IFormFile PathPic { get; set; }
        public DateTime DateTime { get; set; }

    }
}
