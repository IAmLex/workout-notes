using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutService.Contexts;
using WorkoutService.DTOs;
using WorkoutService.Helpers;
using WorkoutService.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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

        [HttpGet, Authorize]
        public IEnumerable<ShowWorkoutDTO> Get()
        {
            return _workoutService.Get();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var workout = _workoutService.Get(id);
            if (workout == null) return NotFound("Workout not found.");

            return Ok(workout);
        }

        [HttpPost, Authorize]
        public ShowWorkoutDTO Post([FromBody] CreateWorkoutDTO workout)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            return _workoutService.Post(userId, workout);
        }

        [HttpPut("{id}"), Authorize]
        public IActionResult Put(int id, [FromBody] UpdateWorkoutDTO updateWorkoutDTO)
        {
            if (updateWorkoutDTO == null) return BadRequest("No workout provided");

            var workout = _workoutService.Get(id);
            if (workout == null) return NotFound("Workout not found.");
            
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (workout.User.Id != userId) return Forbid("You are not authorized to update this workout.");

            var result = _workoutService.Put(id, WorkoutHelper.ToModel(updateWorkoutDTO));

            return Ok(result);
        }
    }
}
