using Microsoft.AspNetCore.Mvc;
using WorkoutService.Contexts;
using WorkoutService.Models;

namespace WorkoutService.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly WorkoutContext _context;

        public UserController(WorkoutContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.Users;
        }
    }
}