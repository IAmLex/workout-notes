using WorkoutService.DTOs;
using WorkoutService.Models;

namespace WorkoutService.Helpers
{
    public class WorkoutHelper
    {
        public static ShowWorkoutDTO ToDTO(Workout workout)
        {
            return new ShowWorkoutDTO
            {
                Id = workout.Id,
                Name = workout.Name,
                Exercises = workout.Exercises.Select(x => ExerciseHelper.ToDTO(x)).ToList(),
                CreatedAt = workout.CreatedAt
            };
        }

        public static List<ShowWorkoutDTO> ToDTO(List<Workout> workouts)
        {
            return workouts.Select(x => ToDTO(x)).ToList();
        }
    }
}