using System.ComponentModel.DataAnnotations;

namespace CarAuction.Models.DTO
{
    public class RegisterUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; } 
        [Required]
        public string Nationality { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public int RoleId { get; set; } = 1;
        //1 Admin 
        //2 Moderator
        //3 Użytkownik
    }
}
