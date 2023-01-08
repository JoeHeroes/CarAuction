using CarAuction.Models.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarAuction.Models.DTO
{
    public class SearchVehicleDto
    {
        public string SearchName { get; set; } = null;
        public string Producer { get; set; } = null;
        public List<SelectListItem> ProducerSelectList { get; set; }
        public string Damage { get; set; } = null;
        public List<SelectListItem> DamageSelectList { get; set; }
        public string LocationName { get; set; } = null;
        public List<SelectListItem> LocationSelectList { get; set; }
        public int RegistrationYear { get; set; } = 0;
        public List<SelectListItem> RegistrationYearSelectList { get; set; }
        public int SinceYear { get; set; } = 2000;
        public List<SelectListItem> SinceYearSelectList { get; set; }
        public int ToYear { get; set; } = 2022;
        public List<SelectListItem> ToYearSelectList { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string SortBy { get; set; } = null!;
        public SortDirection SortDirection { get; set; }
    }
}
