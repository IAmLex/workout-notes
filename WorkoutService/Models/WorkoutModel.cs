namespace WorkoutService.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Exercise> Exercises { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public Workout()
        {
            Exercises = new List<Exercise>();
        }
    }
}
