using LearnApi.Domain.Models;
using LearnApi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AutoMapper;

namespace LearnApi.Controllers
{

    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public AuthController(ITokenService tokenService, IPasswordHasher passwordHasher, IUserRepository userRepository, IMapper mapper)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user is null ||
                !_passwordHasher.ComparePassword(password, user.PasswordHash))
            {
                return NotFound("username or password invalid");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user is not null) return BadRequest("username already taken");

            var passwordHash = _passwordHasher.HashPassword(password);

            user = new User(
                username: username,
                passwordHash: passwordHash
            );

            _userRepository.Add(user);

            return Ok(_mapper.Map<UserDTO>(user));
        }
    }
}