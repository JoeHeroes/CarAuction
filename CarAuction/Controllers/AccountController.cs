using Microsoft.AspNetCore.Mvc;
using CarAuction.Services;
using CarAuction.Models.DTO;

namespace UniAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController: ControllerBase
    {

        private readonly IAccountServices _service;

        public AccountController(IAccountServices service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto) 
        {
            _service.RegisterUser(dto);
            return Ok();
        }
        
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _service.GeneratJwt(dto);

            return Ok(token);
        }
       
    }
}
