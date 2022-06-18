namespace WorkoutService.DTOs
{
    public class ShowWorkoutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ShowExerciseDTO> Exercises { get; set; }
        public ShowUserDTO User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}