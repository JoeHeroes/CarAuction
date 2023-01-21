using CarAuction.Models.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace CarAuction.Models.DTO
{
    public class SearchVehicleDto
    {
        public string SearchName { get; set; } = null;
        public string Producer { get; set; } = null;
        public List<SelectListItem> ProducerSelectList { get; set; }
        public string Damage { get; set; } = null;
        public List<SelectListItem> DamageSelectList { get; set; }
        [DisplayName("Location")]
        public string LocationName { get; set; } = null;
        public List<SelectListItem> LocationSelectList { get; set; }
        [DisplayName("Registration Year")]
        public int RegistrationYear { get; set; } = 0;
        public List<SelectListItem> RegistrationYearSelectList { get; set; }
        [DisplayName("Since Year")]
        public int SinceYear { get; set; } = 2000;
        public List<SelectListItem> SinceYearSelectList { get; set; }
        [DisplayName("To Year")]
        public int ToYear { get; set; } = 2022;
        public List<SelectListItem> ToYearSelectList { get; set; }
        [DisplayName("Page Number")]
        public int PageNumber { get; set; } = 1;
        [DisplayName("Page Size")]
        public int PageSize { get; set; } = 5;
        [DisplayName("Sort By")]
        public string SortBy { get; set; } = null!;
        [DisplayName("Sort Direction")]
        public SortDirection SortDirection { get; set; }
    }
}
