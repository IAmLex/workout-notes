using WorkoutService.Interfaces;
using WorkoutService.Models;

namespace WorkoutService.Stubs
{
    public class WorkoutRepositoryStub : IWorkoutRepository
    {
        private List<Workout> _workouts = new List<Workout>();

        public WorkoutRepositoryStub() 
        {
            var workoutCount = 100;
            var exercise = new Exercise() {
                Id = 1,
                Name = "Bench Press",
                Sets = 3,
                Reps = 12,
                CreatedAt = new DateTime(2020, 1, 1)
            };

            for (var i = 0; i < workoutCount; i++)
            {
                _workouts.Add(new Workout
                {
                    Id = i,
                    Name = $"Workout {i}",
                    Exercises = new List<Exercise>() { exercise },
                    CreatedAt = new DateTime(2020, 1, 1)
                });
            }
        }

        public List<Workout> Get()
        {
            return _workouts;
        }

        public Workout? Get(int id)
        {
            return _workouts.FirstOrDefault(w => w.Id == id);
        }

        public Workout Post(Workout workout)
        {
            throw new NotImplementedException();
        }
    }
}