using WorkoutService.DTOs;
using WorkoutService.Helpers;
using WorkoutService.Interfaces;
using WorkoutService.Models;

namespace WorkoutService.Services
{
    public class ExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public ShowExerciseDTO Post(CreateExerciseDTO exerciseDTO)
        {
            var exercise = new Exercise
            {
                Name = exerciseDTO.Name,
                Sets = exerciseDTO.Sets,
                Reps = exerciseDTO.Reps,
                CreatedAt = DateTime.Now,
            };

            _exerciseRepository.Post(exercise);

            return ExerciseHelper.ToDTO(exercise);
        }
    }
}