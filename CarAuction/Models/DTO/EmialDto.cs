using CarAuction.Migrations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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
