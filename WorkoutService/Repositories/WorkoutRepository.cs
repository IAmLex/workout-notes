using WorkoutService.Contexts;
using WorkoutService.Interfaces;
using WorkoutService.Models;

namespace WorkoutService.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly WorkoutContext _context;

        public WorkoutRepository(WorkoutContext context)
        {
            _context = context;
        }

        public List<Workout> Get()
        {
            var workouts = _context.Workouts.ToList();
            if (workouts == null) throw new Exception("No workouts found");

            return workouts;
        }

        public Workout? Get(int id)
        {
            return _context.Workouts.Find(id);
        }

        public Workout Post(Workout workout)
        {
            _context.Workouts.Add(workout);
            _context.SaveChanges();

            return workout;
        }
    }
}