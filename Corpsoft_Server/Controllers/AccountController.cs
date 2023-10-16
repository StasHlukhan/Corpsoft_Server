using Corpsoft_Server.Dto;
using Corpsoft_Server.Model;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener_Server.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Corpsoft_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Невірні дані користувача.");
            }

            var existingUser = _userRepository.GetUserByUsername(request.Username);
            if (existingUser != null)
            {
                return Conflict("Користувач з таким іменем вже існує.");
            }

            var user = new User
            {
                Username = request.Username,
                Password = request.Password
            };

            _userRepository.AddUser(user);

            return Ok(new { message = "Успішно зареєструвались", user });
        }

        [HttpPost("login")]
        public IActionResult UserLogin([FromBody] UserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = _userRepository.GetUserByUsername(request.Username);

            if (user != null && user.Password == request.Password)
            {
                return Ok(new { message = "Успішно ввійшли", user });
            }
            else
            {
                return BadRequest("Неправильний логін або пароль");
            }
        }

        [HttpGet("getUsers")]
        public  IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            return Ok(users);
        }
    }
}
