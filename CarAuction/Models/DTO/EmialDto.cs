using System.ComponentModel.DataAnnotations;

namespace CarAuction.Models.DTO
{
    public class EmialDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        [MaxLength(200)]
        public string Body { get; set; }

    }
}
