using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserService.Contexts;
using UserService.DTOs;
using UserService.Helpers;
using UserService.Interfaces;
using UserService.Models;
using UserService.Services;
using UserService.Types;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService_ _userService;
        private readonly UserContext _context;
        private readonly IMessageProducer _producer;
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;

        public UserController(UserService_ userService,
                              IMessageProducer producer,
                              UserContext context,
                              ILogger<UserController> logger,
                              IConfiguration configuration)
        {
            _userService = userService;
            _producer = producer;
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.Users;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return BadRequest("User not found.");

            var showUserDTO = UserHelper.ToShowDTO(user);

            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(user.Email));
            showUserDTO.GravatarUrl = $"https://www.gravatar.com/avatar/{BitConverter.ToString(hash).Replace("-", "").ToLower()}?s=200";

            return Ok(showUserDTO);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDTO loginUserDTO)
        {
            string username = loginUserDTO.Username;
            string password = loginUserDTO.Password;

            var user = _userService.Get(username);
            if (user == null) { return BadRequest("User not found"); }

            // verify password hash
            var isValidPassword = false;
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                isValidPassword = computedHash.SequenceEqual(user.PasswordHash);
            }
            if (!isValidPassword) { return BadRequest("Invalid password"); }

            // create token
            string token = CreateToken(user);

            // set refresh token

            // return token
            return Ok(token);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserDTO userDTO)
        {
            UserDTO user = _userService.Post(userDTO);
            if (user == null) { return BadRequest(); }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            User? userToUpdate = _context.Users.Find(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            userToUpdate.Username = user.Username;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;

            _context.SaveChanges();

            _producer.SendMessage(UserHelper.ToDTO(userToUpdate), RoutingKeyType.UserUpdated);

            return Ok(userToUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User? userToDelete = _context.Users.Find(id);
            if (userToDelete == null)
            {
                return NotFound();
            }
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();

            _producer.SendMessage(UserHelper.ToDTO(userToDelete), RoutingKeyType.UserDeleted);

            return Ok(userToDelete);
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}