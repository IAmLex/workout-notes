using WorkoutService.DTOs;
using WorkoutService.Helpers;
using WorkoutService.Interfaces;
using WorkoutService.Models;

namespace WorkoutService.Services
{
    public class WorkoutService_
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public WorkoutService_(IWorkoutRepository workoutRepository, IExerciseRepository exerciseRepository)
        {
            _workoutRepository = workoutRepository;
            _exerciseRepository = exerciseRepository;
        }

        public List<ShowWorkoutDTO> Get()
        {
            return WorkoutHelper.ToDTO(_workoutRepository.Get());
        }

        public ShowWorkoutDTO? Get(int id)
        {
            var workout = _workoutRepository.Get(id);
            if (workout == null) return null;

            return WorkoutHelper.ToDTO(workout);
        }

        public ShowWorkoutDTO Post(CreateWorkoutDTO workoutDTO)
        {
            var workout = new Workout
            {
                Name = workoutDTO.Name,
                CreatedAt = DateTime.Now,
            };

            if (workoutDTO.ExerciseIDs != null)
            {
                foreach (var id in workoutDTO.ExerciseIDs)
                {
                    workout.Exercises.Add(_exerciseRepository.Get(id));
                }
            }

            _workoutRepository.Post(workout);

            return WorkoutHelper.ToDTO(workout);
        }
    }
}