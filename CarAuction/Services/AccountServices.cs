using CarAuction.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarAuction.Authorization;
using CarAuction.Entites;
using CarAuction.Models.DTO;

namespace CarAuction.Services
{

    public interface IAccountServices
    {
        string GeneratJwt(LoginDto dto);
        void RegisterUser(RegisterUserDto dto);
    }
    public class AccountServices : IAccountServices
    {
        private readonly AuctionDbContext  dbContext;
        private readonly IPasswordHasher<User>  passwordHasher;
        private readonly AuthenticationSettings  authenticationSetting;
        public AccountServices(AuctionDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSetting)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.authenticationSetting = authenticationSetting;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PasswordHash = dto.Password,
                RoleId = dto.RoleId

            };

        var hashedPass = this.passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPass;
            this.dbContext.Users.Add(newUser);
            this.dbContext.SaveChanges();
        }
        
        public string GeneratJwt(LoginDto dto)
        {
            var user = this.dbContext
                 .Users
                 .Include(u => u.Role)
                 .FirstOrDefault(u => u.Email == dto.Email);


            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }


            var result = this.passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var clasims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name} "),
                //new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
                
            };


            if (!string.IsNullOrEmpty(user.Nationality))
            {
                clasims.Add(
                    new Claim("Nationality", user.Nationality)
                    );
            }

           

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.authenticationSetting.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(this.authenticationSetting.JwtExpireDays);


            var token = new JwtSecurityToken(this.authenticationSetting.JwtIssuer,
                this.authenticationSetting.JwtIssuer,
                clasims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenHander = new JwtSecurityTokenHandler();
            return tokenHander.WriteToken(token);
        }
       
    }
}
