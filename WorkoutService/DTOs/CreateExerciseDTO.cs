namespace WorkoutService.DTOs
{
    public class CreateExerciseDTO
    {
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}