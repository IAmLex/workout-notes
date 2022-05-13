using WorkoutService.DTOs;
using WorkoutService.Models;

namespace WorkoutService.Helpers
{
    public class ExerciseHelper
    {
        public static ShowExerciseDTO ToDTO(Exercise exercise)
        {
            return new ShowExerciseDTO
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                CreatedAt = exercise.CreatedAt
            };
        }

        public static List<ShowExerciseDTO> ToDTO(List<Exercise> exercises)
        {
            return exercises.Select(x => ToDTO(x)).ToList();
        }
    }
}