using Microsoft.AspNetCore.Mvc;
using WorkoutService.Contexts;
using WorkoutService.DTOs;
using WorkoutService.Services;

namespace WorkoutService.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly WorkoutContext _context;
        private readonly WorkoutService_ _workoutService;

        public WorkoutController(WorkoutContext context, WorkoutService_ workoutService)
        {
            _context = context;
            _workoutService = workoutService;
        }

        [HttpGet]
        public IEnumerable<ShowWorkoutDTO> Get()
        {
            return _workoutService.Get();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var workout = _workoutService.Get(id);
            if (workout == null) return NotFound("Workout not found");

            return Ok(workout);
        }

        [HttpPost]
        public ShowWorkoutDTO Post([FromBody] CreateWorkoutDTO workout)
        {
            return _workoutService.Post(workout);
        }
    }
}
