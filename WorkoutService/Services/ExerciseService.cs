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

        public List<ShowExerciseDTO> Get()
        {
            return ExerciseHelper.ToDTO(_exerciseRepository.Get());
        }

        public ShowExerciseDTO? Get(int id)
        {
            var exercise = _exerciseRepository.Get(id);
            if (exercise == null) return null;

            return ExerciseHelper.ToDTO(exercise);
        }

        public ShowExerciseDTO Post(CreateExerciseDTO exerciseDTO)
        {
            var exercise = new Exercise
            {
                Name = exerciseDTO.Name,
                Sets = exerciseDTO.Sets,
                Reps = exerciseDTO.Reps,
                CreatedAt = DateTime.Now
            };

           exercise = _exerciseRepository.Post(exercise);

            return ExerciseHelper.ToDTO(exercise);
        }
    }
}