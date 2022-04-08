using Microsoft.AspNetCore.Mvc;
using UserService.Contexts;
using UserService.Helpers;
using UserService.Interfaces;
using UserService.Models;
using UserService.Types;

namespace UserService.Controllers 
{   
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IMessageProducer _producer;
        private readonly ILogger<UserController> _logger;

        public UserController(IMessageProducer producer, UserContext context, ILogger<UserController> logger)
        {
            _producer = producer;
            _context = context;
            _logger = logger;
        }

        [HttpGet()]
        public IEnumerable<User> Get()
        {
            return _context.Users;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _context.Users.Find(id);

            return user != null ? Ok(user) : NotFound($"User with id {id} not found");
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            _producer.SendMessage(user.ToDTO(), RoutingKeyType.UserCreated);

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

            _producer.SendMessage(userToUpdate.ToDTO(), RoutingKeyType.UserUpdated);

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

            _producer.SendMessage(userToDelete.ToDTO(), RoutingKeyType.UserDeleted);

            return Ok(userToDelete);
        }
    }
}