using Microsoft.AspNetCore.Mvc;
using WorkoutService.Contexts;
using WorkoutService.DTOs;
using WorkoutService.Services;

namespace WorkoutService.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly ExerciseService _exerciseService;

        public ExerciseController(ExerciseService exerciseService)
        {
            _exerciseService = exerciseService;

            // Actions tries: 2
        }

        [HttpPost]
        public ShowExerciseDTO Post([FromBody] CreateExerciseDTO exerciseDTO)
        {
            var exercise = _exerciseService.Post(exerciseDTO);
            return exercise;
        }
    }
}
