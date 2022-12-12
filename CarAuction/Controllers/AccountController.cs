using CarAuction;
using CarAuction.Exceptions;
using CarAuction.Models;
using CarAuction.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UniAPI.Controllers
{
    [Route("Account")]
    public class AccountController: Controller
    {
        private readonly AuctionDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasherUser;
        private readonly AuthenticationSettings authenticationSetting;

        public AccountController(AuctionDbContext dbContext, IPasswordHasher<User> passwordHasherUser, AuthenticationSettings authenticationSetting)
        {
            this.dbContext = dbContext;
            this.passwordHasherUser = passwordHasherUser;
            this.authenticationSetting = authenticationSetting;
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("id");
            return RedirectToAction("Login");
        }

        [Route("Welcome")]
        public IActionResult Welcome()
        {
            ViewBag.username = HttpContext.Session.GetString("name");
            return View("Welcome");
        }

        [Route("AccountSettings")]
        public IActionResult AccountSettings()
        {
            var account = dbContext.Users.FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));
            return View(account);
        }

        [HttpPost]
        [Route("AccountSettings")]
        public IActionResult AccountSettings(EditUserDto dto)
        {
            if (ModelState.IsValid)
            {
                var account = dbContext.Users.FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));
                account.Nationality = dto.Nationality;
                account.FirstName = dto.FirstName;
                account.LastName = dto.LastName;

                dbContext.SaveChanges();
            }
            return View("AccountSettings");
        }



        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult Register(RegisterUserDto dto)
        {
            if (ModelState.IsValid)
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

                var hashedPass = this.passwordHasherUser.HashPassword(newUser, dto.Password);

                newUser.PasswordHash = hashedPass;
                this.dbContext.Users.Add(newUser);
                this.dbContext.SaveChanges();

                return RedirectToAction("Welcome");
            }
            ViewBag.msg = "Invalid";
            return View("Login");
        }

        [HttpPost]
        [Route("LoginUser")]
        public IActionResult Login(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = this.dbContext
                                .Users
                                .Include(u => u.Role)
                                .FirstOrDefault(u => u.Email == dto.Email);

                if (user is null)
                {
                    ViewBag.msg = "Email or password is invalid.";
                    return View("Login");
                }

                var result = this.passwordHasherUser.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    ViewBag.msg = "Email or password is invalid.";
                    return View("Login");
                }
                HttpContext.Session.SetString("name", user.LastName+" "+user.FirstName);
                HttpContext.Session.SetString("id", user.Id.ToString());
                return RedirectToAction("Welcome");
            }
            return View(dto);
        }
    }
}
