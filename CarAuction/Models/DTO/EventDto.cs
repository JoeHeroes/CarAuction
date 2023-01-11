using System.ComponentModel.DataAnnotations;

namespace CarAuction.Models.DTO
{
    public class EventDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
       
        public string BackgroundColor { get; set; }
    }
}
