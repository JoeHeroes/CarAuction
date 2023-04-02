using CarAuction;
using CarAuction.Models;
using CarAuction.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UniAPI.Controllers
{
    public class AccountController: Controller
    {
        private readonly AuctionDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasherUser;
        private readonly AuthenticationSettings authenticationSetting;
        private readonly IWebHostEnvironment webHost;

        public AccountController(AuctionDbContext dbContext, IPasswordHasher<User> passwordHasherUser, AuthenticationSettings authenticationSetting, IWebHostEnvironment webHost)
        {
            this.dbContext = dbContext;
            this.passwordHasherUser = passwordHasherUser;
            this.authenticationSetting = authenticationSetting;
            this.webHost = webHost;
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
            HttpContext.Session.Remove("role");
            return RedirectToAction("Login");
        }

        [Route("Welcome")]
        public IActionResult Welcome()
        {
            ViewBag.username = HttpContext.Session.GetString("name");
            return View("Welcome");
        }
        [Route("Profile")]
        public IActionResult Profile()
        {
            var account = this.dbContext.Users.FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));

            var role = this.dbContext.Roles.FirstOrDefault(x => x.Id == account.RoleId);

            UserView accountView = new UserView()
            {
                Id = account.Id,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                DateOfBirth = account.DateOfBirth,
                Nationality = account.Nationality,
                PasswordHash = account.PasswordHash,
                Role = role.Name,
                ProfileImg = account.ProfileImg,
            };
            return View(accountView);
        }


        [Route("ProfileEdit")]
        public IActionResult ProfileEdit(int id)
        {
            var account = dbContext.Users.FirstOrDefault(x => x.Id == id);

            return View(account);
        }

        [HttpPost]
        [Route("ProfileEdit")]
        public IActionResult ProfileEdit(EditUserDto dto)
        {
            if (ModelState.IsValid)
            {
                if (dto.DateOfBirth > DateTime.Now.Date)
                {
                    ViewBag.msg = "Your are time traveler?";
                    return View("ProfileEdit");
                }

                var account = dbContext.Users.FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));
                account.Nationality = dto.Nationality;
                account.FirstName = dto.FirstName;
                account.LastName = dto.LastName;
                account.DateOfBirth = dto.DateOfBirth;
                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }
            }
            return View("ProfileEdit");
        }

        [Route("SelectPicture")]
        public IActionResult SelectPicture()
        {
            var dto = new PictureDto();

            return View(dto);
        }

        [HttpPost]
        [Route("SelectPicture")]
        public IActionResult SelectPicture(PictureDto dto)
        {


            string stringFileName = UploadFile(dto);
            if (ModelState.IsValid)
            {
                var model = this.dbContext.Users.FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));
                model.ProfileImg = stringFileName;

                HttpContext.Session.SetString("img", stringFileName);

                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("Profile");
            }

            return View(dto);

        }


        private string UploadFile(PictureDto dto)
        {
            string fileName = null;
            if (dto.PathPic != null)
            {
                string uploadDir = Path.Combine(webHost.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + dto.PathPic.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dto.PathPic.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        [Route("RestartPassword")]
        public IActionResult RestartPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("RestartPassword")]
        public IActionResult RestartPassword(RestartPasswordDto dto)
        {

            if (ModelState.IsValid)
            {

                if (dto.NewPassword != dto.ConfirmNewPassword)
                {
                    ViewBag.msg = "New Password must be the same.";
                    return View("RestartPassword");
                }


                if (dto.OldPassword == dto.NewPassword)
                {
                    ViewBag.msg = "New and Old Password and couldn't be the same";
                    return View("RestartPassword");
                }

                var account = dbContext.Users.FirstOrDefault(x => x.Id == int.Parse(HttpContext.Session.GetString("id")));


                var result1 = this.passwordHasherUser.VerifyHashedPassword(account, account.PasswordHash, dto.OldPassword);
                if (result1 == PasswordVerificationResult.Failed)
                {
                    ViewBag.msg = "Old password is invalid.";
                    return View("RestartPassword");
                }

                account.PasswordHash = this.passwordHasherUser.HashPassword(account, dto.NewPassword); ;
                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("Profile");

            }
            return View("RestartPassword");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = this.dbContext
                                .Users
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
                HttpContext.Session.SetString("name", "  " + user.FirstName + " " + user.LastName);
                HttpContext.Session.SetString("id", user.Id.ToString());
                if (user.RoleId == 1)
                {
                    HttpContext.Session.SetString("role", "Buyer");
                }
                else if (user.RoleId == 2)
                {
                    HttpContext.Session.SetString("role", "Seller");
                }
                else
                {
                    HttpContext.Session.SetString("role", "Admin");
                }
                    
                return RedirectToAction("Welcome");
            }
            return View("Login");
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterUserDto dto)
        {
            var user = this.dbContext
                                .Users
                                .FirstOrDefault(u => u.Email == dto.Email);

            if(user != null)
            {
                ViewBag.msg = "Email is taken";
                return View("Register");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                ViewBag.msg = "Invalid Password";
                return View("Register");
            }

            if (dto.DateOfBirth>DateTime.Now.Date)
            {
                ViewBag.msg = "Are you time traveler?";
                return View("Register");
            }

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
                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw new DbUpdateException("Error DataBase", e);
                }

                return RedirectToAction("Welcome");
            }
            ViewBag.msg = "Invalid";
            return View("Register");
        }
    }
}
