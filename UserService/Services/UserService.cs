using System.Security.Cryptography;
using UserService.Contexts;
using UserService.DTOs;
using UserService.Helpers;
using UserService.Interfaces;
using UserService.Models;
using UserService.Types;

namespace UserService.Services
{
    public class UserService_
    {
        private readonly UserContext _context;
        private readonly IMessageProducer _producer;
        private readonly ILogger<UserService_> _logger;
        public UserService_(UserContext context, IMessageProducer producer, ILogger<UserService_> logger)
        {
            _context = context;
            _producer = producer;
            _logger = logger;
        }

        public User? Get(string username) 
        {
            return _context.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public UserDTO Post(CreateUserDTO createUserDTO)
        {
            CreatePasswordHash(createUserDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User() {
                Username = createUserDTO.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = createUserDTO.Email,
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            UserDTO userDTO = UserHelper.ToDTO(user);

            _producer.SendMessage(userDTO, RoutingKeyType.UserCreated);

            return userDTO;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

