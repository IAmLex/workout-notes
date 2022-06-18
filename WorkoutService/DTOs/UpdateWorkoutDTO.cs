namespace WorkoutService.DTOs
{
    public class UpdateWorkoutDTO 
    {
        public string? Name { get; set; }
        public List<int>? ExerciseIDs { get; set; }
    }
}