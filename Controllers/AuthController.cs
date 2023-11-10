using LearnApi.Domain.Models;
using LearnApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearnApi.Controllers
{

    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "gabriel" && password == "123456")
            {
                var token = _tokenService.GenerateToken(new Employee("gabriel", 25, null));
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}