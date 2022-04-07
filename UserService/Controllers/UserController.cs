using Microsoft.AspNetCore.Mvc;
using UserService.Contexts;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Controllers 
{   
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IMessageProducer _producer;

        public UserController(IMessageProducer producer, UserContext context)
        {
            _producer = producer;
            _context = context;
        }

        [HttpGet()]
        public IEnumerable<User> Get()
        {
            return _context.Users;
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            _producer.SendMessage(user);

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

            // TODO: Inform rabbitMQ

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

            // TODO: Inform rabbitMQ

            return Ok(userToDelete);
        }
    }
}