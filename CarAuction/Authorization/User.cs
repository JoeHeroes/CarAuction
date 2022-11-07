using System;

namespace CarAuction.Authorization
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
    }
}
