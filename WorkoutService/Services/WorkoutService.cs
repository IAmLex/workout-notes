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
        private readonly IUserRepository _userRepository;

        public WorkoutService_(IWorkoutRepository workoutRepository, IExerciseRepository exerciseRepository, IUserRepository userRepository)
        {
            _workoutRepository = workoutRepository;
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
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

        public ShowWorkoutDTO Post(int userId, CreateWorkoutDTO workoutDTO)
        {
            var workout = new Workout
            {
                Name = workoutDTO.Name,
                User = _userRepository.Get(userId),
                CreatedAt = DateTime.Now,
            };

            if (workoutDTO.ExerciseIDs != null)
            {
                foreach (var id in workoutDTO.ExerciseIDs)
                {
                    var exercise = _exerciseRepository.Get(id);
                    if (exercise == null) continue;

                    workout.Exercises.Add(exercise);
                }
            }

            workout = _workoutRepository.Post(workout);

            return WorkoutHelper.ToDTO(workout);
        }

        public Workout? Put(int id, Workout workout)
        {
            var workoutToUpdate = _workoutRepository.Get(id);
            if (workoutToUpdate == null) return null;

            if (workout.Name != null) workoutToUpdate.Name = workout.Name;

            return _workoutRepository.Put(workoutToUpdate);
        }
    }
}