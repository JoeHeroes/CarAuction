using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarAuction.Models.DTO
{
    public class EventDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [DisplayName("Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("Color")]
        public string BackgroundColor { get; set; }
    }
}
