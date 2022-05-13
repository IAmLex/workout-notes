using WorkoutService.Contexts;
using WorkoutService.Interfaces;
using WorkoutService.Models;

namespace WorkoutService.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly WorkoutContext _context;

        public ExerciseRepository(WorkoutContext context)
        {
            _context = context;
        }

        public List<Exercise> Get()
        {
            var exercises = _context.Exercises.ToList();
            if (exercises == null) throw new Exception("No exercises found");

            return exercises;
        }

        public Exercise Get(int id)
        {
            var exercise = _context.Exercises.Find(id);
            if (exercise == null) throw new Exception("Exercise not found");

            return exercise;
        }

        public Exercise Post(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            _context.SaveChanges();

            return exercise;
        }
    }
}