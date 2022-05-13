namespace WorkoutService.DTOs
{
    public class ShowExerciseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}