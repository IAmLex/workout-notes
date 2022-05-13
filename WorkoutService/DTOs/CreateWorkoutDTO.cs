namespace WorkoutService.DTOs
{
    public class CreateWorkoutDTO
    {
        public string Name { get; set; }
        public List<int>? ExerciseIDs { get; set; }
    }
}