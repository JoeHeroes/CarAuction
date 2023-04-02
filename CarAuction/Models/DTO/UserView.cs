using System.ComponentModel;

namespace CarAuction.Models.DTO
{
    public class UserView
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string ProfileImg { get; set; } = null;
    }
}
