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

        [HttpPost]
        public ShowWorkoutDTO Post([FromBody] CreateWorkoutDTO workout)
        {
            return _workoutService.Post(workout);
        }
    }
}
